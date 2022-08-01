using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PMS.Backend.Core.Entities;
using PMS.Backend.Features.Exceptions;
using PMS.Backend.Features.Extensions;

namespace PMS.Backend.Features.Common;

/// <summary>
/// Specifies the contract for the CRUD operations of an <see cref="Entity"/>.
/// </summary>
/// <typeparam name="T">The type of the Entity.</typeparam>
public abstract class Service<T>
    where T : Entity
{
    /// <summary>
    /// The <see cref="DbContext"/> where the <see cref="Entity"/> is stored in.
    /// </summary>
    protected DbContext Context { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="Service{T}"/>.
    /// </summary>
    /// <param name="context">The database context.</param>
    protected Service(DbContext context) => Context = context;

    /// <summary>
    /// Gets a <see cref="IQueryable"/> containing all entities.
    /// </summary>
    /// <returns>A non filtered <see cref="IQueryable"/> of type <typeparamref name="T"/>.</returns>
    public virtual IQueryable<T> GetAll()
    {
        return Context.Set<T>();
    }

    /// <summary>
    /// Finds an entity with a given id.
    /// </summary>
    /// <param name="id">The id of the entity.</param>
    /// <returns>A <see cref="IQueryable"/> containing at most one entity.</returns>
    public virtual IQueryable<T> Find(int id)
    {
        return Context.Set<T>().Where(x => x.Id == id);
    }

    /// <summary>
    /// Creates a new Entity.
    /// </summary>
    /// <param name="input">The DTO to be mapped to an entity.</param>
    /// <typeparam name="TInput">The type of the input DTO.</typeparam>
    /// <typeparam name="TValidator">A validator for the input DTO.</typeparam>
    /// <returns>The newly created entity.</returns>
    /// <exception cref="BadRequestException">
    /// Thrown if the <typeparamref name="TValidator"/> encountered validation errors.
    /// </exception>
    public virtual async Task<T> CreateAsync<TInput, TValidator>(TInput input)
        where TValidator : AbstractValidator<TInput>, new()
    {
        var entity = await Context.ValidateAndMapAsync<T, TInput, TValidator>(input);
        entity.ValidateIds(Context);
        await Context.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Updates an entity.
    /// </summary>
    /// <param name="id">The id of the entity.</param>
    /// <param name="input">The DTO containing the new fields.</param>
    /// <typeparam name="TInput">The type of the input DTO.</typeparam>
    /// <typeparam name="TValidator">A validator for the input DTO.</typeparam>
    /// <returns>The updated entity.</returns>
    /// <exception cref="BadRequestException">
    /// Thrown if the <typeparamref name="TValidator"/> encountered validation errors,
    /// or if the <paramref name="id"/> does not match the id of the <typeparamref name="TInput"/>.
    /// </exception>
    public virtual async Task<T> UpdateAsync<TInput, TValidator>(int id, TInput input)
        where TInput : UpdateDTO
        where TValidator : AbstractValidator<TInput>, new()
    {
        if (id != input.Id)
        {
            throw new BadRequestException(
                $"The query id {id} does not match the DTO id {input.Id}");
        }

        var entity = await Context.ValidateAndMapAsync<T, TInput, TValidator>(input);
        entity.ValidateIds(Context, true);
        await Context.SaveChangesAsync();

        return entity;
    }

    /// <summary>
    /// Deletes an entity.
    /// </summary>
    /// <param name="id">The id of the entity to be deleted.</param>
    public virtual  async Task DeleteAsync(int id)
    {
        if (await Context.Set<T>().FindAsync(id) is { } entity)
        {
            Context.Set<T>().Remove(entity);
            await Context.SaveChangesAsync();
        }
    }
}
