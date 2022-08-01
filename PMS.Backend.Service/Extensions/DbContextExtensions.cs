using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using PMS.Backend.Core.Entities;
using PMS.Backend.Core.Entities.Agency;

namespace PMS.Backend.Service.Extensions;

/// <summary>
/// Extensions for <see cref="DbContext"/> instances.
/// </summary>
/// TODO: Source Generator
public static class DbContextExtensions
{
    private static MethodInfo ModelBuilderEntitySetMethod { get; } =
        typeof(ODataConventionModelBuilder)
            .GetMethod(nameof(ODataConventionModelBuilder.EntitySet))!;

    /// <summary>
    /// Generates a <see cref="EdmModel"/> from a given <see cref="DbContext"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the <see cref="DbContext"/> to generate the model from.
    /// </typeparam>
    /// <returns>An <see cref="EdmModel"/> containing all <see cref="DbSet{TEntity}"/>.</returns>
    public static IEdmModel GetModel<T>()
        where T : DbContext
    {
        var builder = new ODataConventionModelBuilder();

        foreach (var propertyInfo in typeof(T).GetProperties())
        {
            if (IsSubclassOfRawGeneric(typeof(DbSet<>), propertyInfo.PropertyType))
            {
                var entityType = propertyInfo.PropertyType.GenericTypeArguments[0];
                ModelBuilderEntitySetMethod
                    .MakeGenericMethod(entityType)
                    .Invoke(builder, new[] { propertyInfo.Name as object });
            }
        }

        return builder.GetEdmModel();
    }

    private static bool IsSubclassOfRawGeneric(Type generic, Type? toCheck)
    {
        while (toCheck != null && toCheck != typeof(object))
        {
            var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
            if (generic == cur)
            {
                return true;
            }

            toCheck = toCheck.BaseType;
        }

        return false;
    }
}
