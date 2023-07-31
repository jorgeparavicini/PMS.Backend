using HotChocolate;
using PMS.Backend.Core.Entities;

namespace PMS.Backend.Features.Exceptions;

/// <summary>
///     Exception for when an entity is not found.
/// </summary>
/// <typeparam name="T">
///     The type of the entity that was not found.
/// </typeparam>
public class NotFoundException<T> : GraphQLException
    where T : Entity
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="NotFoundException{T}"/> class.
    /// </summary>
    /// <param name="id">
    ///     The id of the entity that was not found.
    /// </param>
    public NotFoundException(int id)
        : base(ErrorBuilder.New()
            .SetMessage($"{typeof(T).Name} not found with id {id}.")
            .SetCode("ENTITY_NOT_FOUND")
            .Build())
    {
    }
}
