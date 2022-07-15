using System.ComponentModel.DataAnnotations;
using FluentResults;

namespace PMS.Backend.Core.Entities;

/// <summary>
/// The base class for all entities containing audit fields and helper methods.
/// </summary>
public class Entity
{
    /// <summary>
    /// A timestamp used for concurrency checking.
    /// </summary>
    [Timestamp]
    public byte[] TimeStamp { get; set; } = null!;
    
    // TODO: Audit 
    /// <summary>
    /// Validates the entity making sure no attributes are violated.
    /// </summary>
    /// <returns>
    /// A result containing a new-line separated string with all validation errors if it fails,
    /// otherwise ok.
    /// </returns>
    public Result Validate()
    {
        var validationResults = new List<ValidationResult>();
        var success = Validator.TryValidateObject(
            this,
            new ValidationContext(this),
            validationResults, 
            true);

        if (success)
        {
            return Result.Ok();
        }

        var validationErrors = validationResults.Select(x => x.ErrorMessage);
        return Result.Fail(string.Join('\n', validationErrors));
    }
}