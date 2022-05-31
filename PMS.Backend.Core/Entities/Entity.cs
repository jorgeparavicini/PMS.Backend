using System.ComponentModel.DataAnnotations;
using FluentResults;

namespace PMS.Backend.Core.Entities;

public class Entity
{
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