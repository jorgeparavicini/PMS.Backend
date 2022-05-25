using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

// ReSharper disable UnusedParameter.Global

[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace PMS.Backend.Features.Frontend;

public static class Conventions
{
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ApiConventionNameMatch((ApiConventionNameMatchBehavior.Prefix))]
    public static void Get() {}
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ApiConventionNameMatch((ApiConventionNameMatchBehavior.Prefix))]
    public static void Find([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix)] int id) {}
}