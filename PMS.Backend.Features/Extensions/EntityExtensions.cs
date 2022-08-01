using System.Reflection;
using System.Runtime.ExceptionServices;
using Detached.Annotations;
using Detached.Mappers.EntityFramework;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PMS.Backend.Core.Entities;
using PMS.Backend.Features.Exceptions;

namespace PMS.Backend.Features.Extensions;

/// <summary>
/// Extensions for Entity types.
/// </summary>
/// TODO: Use source generators to improve performance.
/// Source Generator projects must be able to be written in .NET 6.0
public static class EntityExtensions
{
    private static readonly MethodInfo ValidateIdMethod = typeof(EntityExtensions)
        .GetMethods(BindingFlags.NonPublic | BindingFlags.Static)
        .First(x => x.Name == nameof(ValidateId));

    private static readonly MethodInfo ValidateAggregateEnumerableMethod = typeof(EntityExtensions)
        .GetMethods(BindingFlags.NonPublic | BindingFlags.Static)
        .First(x => x.Name == nameof(ValidateAggregateEnumerable));

    private static readonly MethodInfo ValidateCompositeEnumerableMethod = typeof(EntityExtensions)
        .GetMethods(BindingFlags.NonPublic | BindingFlags.Static)
        .First(x => x.Name == nameof(ValidateCompositeEnumerable));

    /// <summary>
    /// Validates an input DTO and maps it to an entity stored in the context.
    /// </summary>
    /// <param name="context">The context where the entity should be added.</param>
    /// <param name="input">The input DTO.</param>
    /// <typeparam name="TEntity">The type of the Entity.</typeparam>
    /// <typeparam name="TInput">The type of the input DTO.</typeparam>
    /// <typeparam name="TValidator">The type of the validator for the input DTO.</typeparam>
    /// <returns>The mapped entity.</returns>
    /// <exception cref="BadRequestException">
    /// Thrown if <see cref="TValidator"/> encountered validation errors.
    /// </exception>
    /// <remarks>
    /// The changes will not be saved automatically, however, the mapped entity will be added to
    /// the context. In order to persist the changes only a call to SaveChangesAsync is required.
    /// </remarks>
    public static async Task<TEntity> ValidateAndMapAsync<TEntity, TInput, TValidator>(
        this DbContext context,
        TInput input)
        where TEntity : Entity
        where TValidator : AbstractValidator<TInput>, new()
    {
        var validator = new TValidator();
        var validationResult = await validator.ValidateAsync(input);

        if (!validationResult.IsValid)
        {
            throw new BadRequestException(validationResult.ToString());
        }

        var entity = await context.MapAsync<TEntity>(input);
        entity.ValidateIds(context);
        return entity;
    }

    /// <summary>
    /// Validates the IDs for all aggregate and composite relations in a property.
    /// </summary>
    /// <param name="entity">The entity to validate.</param>
    /// <param name="context">The database context where this entity is part of.</param>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <typeparam name="TContext">The type of the <see cref="DbContext"/>.</typeparam>
    public static void ValidateIds<T, TContext>(this T entity, TContext context)
        where T : Entity
        where TContext : DbContext
    {
        try
        {
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                if (property.PropertyType.IsSubclassOf(typeof(Entity)))
                {
                    ValidateEntity(property, context, entity);
                }
                else if (typeof(IEnumerable<Entity>).IsAssignableFrom(property.PropertyType))
                {
                    ValidateEnumerable(property, context, entity);
                }
            }
        }
        catch (TargetInvocationException e)
        {
            ExceptionDispatchInfo.Capture(e.InnerException!).Throw();
        }
    }

    private static void ValidateEntity<TContext>(
        PropertyInfo property,
        TContext context,
        Entity entity)
        where TContext : DbContext
    {
        if (property.GetCustomAttributes(false).Any(x => x is AggregationAttribute))
        {
            ValidateAggregate(property, context, entity);
        }
        else if (property.GetCustomAttributes(false)
                 .Any(x => x is CompositionAttribute))
        {
            ValidateComposite(property, context, entity);
        }
    }

    private static void ValidateAggregate<TContext>(
        PropertyInfo property,
        TContext context,
        Entity entity)
        where TContext : DbContext
    {
        if (entity.GetType().GetProperty($"{property.Name}Id") is { } idProperty)
        {
            ValidateIdMethod
                .MakeGenericMethod(property.PropertyType, context.GetType())
                .Invoke(null, new[] { context, idProperty.GetValue(entity) });
        }
    }

    private static void ValidateComposite<TContext>(
        PropertyInfo property,
        TContext context,
        Entity parentEntity)
        where TContext : DbContext
    {
        if (property.GetValue(parentEntity) is Entity entity)
        {
            if (entity.Id == 0) return;
            ValidateIdMethod.MakeGenericMethod(property.PropertyType, context.GetType())
                .Invoke(null, new[] { context, entity.Id as object });
        }
    }

    private static void ValidateEnumerable<TContext>(
        PropertyInfo property,
        TContext context,
        Entity parentEntity)
        where TContext : DbContext
    {
        var entities = property.GetValue(parentEntity);
        var enumerableType = entities!.GetType().GetGenericArguments()[0];
        if (property.GetCustomAttributes(false).Any(x => x is AggregationAttribute))
        {
            ValidateAggregateEnumerableMethod.MakeGenericMethod(enumerableType, typeof(TContext))
                .Invoke(null, new[] { context, entities });
        }
        else if (property.GetCustomAttributes(false).Any(x => x is CompositionAttribute))
        {
            ValidateCompositeEnumerableMethod.MakeGenericMethod(enumerableType, typeof(TContext))
                .Invoke(null, new[] { context, entities });
        }
    }

    private static void ValidateAggregateEnumerable<T, TContext>(
        TContext context,
        IEnumerable<T> entities)
        where T : Entity
        where TContext : DbContext
    {
        foreach (var entity in entities)
        {
            ValidateId<T, TContext>(context, entity.Id);
        }
    }

    private static void ValidateCompositeEnumerable<T, TContext>(
        TContext context,
        IEnumerable<T> entities)
        where T : Entity
        where TContext : DbContext
    {
        foreach (var entity in entities)
        {
            if (entity.Id != 0)
            {
                ValidateId<T, TContext>(context, entity.Id);
            }

            entity.ValidateIds(context);
        }
    }

    private static void ValidateId<T, TContext>(TContext context, int id)
        where T : Entity
        where TContext : DbContext
    {
        if (!context.Set<T>().Any(x => x.Id == id))
        {
            throw new NotFoundException(
                $"Could not find an entity of type {typeof(T).Name} with id {id}");
        }
    }
}
