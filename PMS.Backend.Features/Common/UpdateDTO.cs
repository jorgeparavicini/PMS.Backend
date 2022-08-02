namespace PMS.Backend.Features.Common;

/// <summary>
/// A base record for all Update DTOs
/// </summary>
/// <param name="Id">The id of the entity to update.</param>
public abstract record UpdateDTO(int Id);
