using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
// ReSharper disable UnusedParameter.Global

[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace PMS.Backend.Features.Frontend;

public static class Conventions
{
    [ProducesResponseType(StatusCodes.Status200OK)]
    public static void GetAllAsync() {}
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public static void FindAsync(int id) {}
}