using HotChocolate;
using PMS.Backend.Core.Entities;

namespace PMS.Backend.Features.Exceptions;

/// <summary>
///     Exception for when the last entry of a child entity is about to be deleted.
/// </summary>
/// <typeparam name="TParent">
///     The type of the parent entity.
/// </typeparam>
/// <typeparam name="TChild">
///     The type of the child entity.
/// </typeparam>
public class LastEntryDeletionException<TParent, TChild> : GraphQLException
    where TParent : Entity
    where TChild : Entity
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="LastEntryDeletionException{TParent,TChild}"/> class.
    /// </summary>
    /// <param name="parentId">
    ///     The id of the parent entity.
    /// </param>
    /// <param name="childId">
    ///     The id of the child entity.
    /// </param>
    public LastEntryDeletionException(int parentId, int childId)
        : base(ErrorBuilder.New()
            .SetMessage(
                $"Cannot delete last {typeof(TChild).Name} ({childId}) of {typeof(TParent).Name} ({parentId}). At least one {typeof(TChild).Name} must exist.")
            .SetCode("LAST_ENTRY_DELETION")
            .Build())
    {
    }
}
