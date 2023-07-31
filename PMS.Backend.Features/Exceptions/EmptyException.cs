using HotChocolate;
using PMS.Backend.Core.Entities;

namespace PMS.Backend.Features.Exceptions;

/// <summary>
///     Exception for when a property is empty.
/// </summary>
/// <typeparam name="T">
///     The type of the entity that was empty.
/// </typeparam>
public class EmptyException<T> : GraphQLException
    where T : Entity
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="EmptyException{T}"/> class.
    /// </summary>
    public EmptyException()
        : base(new ErrorBuilder().SetMessage($"At least one {typeof(T).Name} must be provided.").Build())
    {
    }
}
