using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

// ReSharper disable UnusedParameter.Global

namespace PMS.Backend.Features;

/// <summary>
/// The REST API Conventions used in all features.
/// </summary>
/// <remarks>
/// This will add the appropriate response types to all endpoints.
/// In special cases other response types may have to be added.
/// </remarks>
[ExcludeFromCodeCoverage]
public static class Conventions
{
    #region GET

    /// <summary>
    /// A convention for getting a list of all entities in a collection.
    /// </summary>
    /// <remarks>
    /// This convention may only produce a <c>200 Ok</c>, a <c>204 No content</c> or a
    /// <c>500 internal error</c> response.
    /// <para>To apply this convention the method must start with <c>GetAll</c></para>
    /// </remarks>
    /// <example><c>GetAllReservations</c></example>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
    public static void GetAll()
    {
    }

    /// <summary>
    /// A convention for finding a specific entity in a collection.
    /// </summary>
    /// <remarks>
    /// This convention may only produce a <c>200 Ok</c>, a <c>404 Not found</c> or a
    /// <c>500 internal error</c> response.
    /// <para>
    /// To apply this convention the method must start with <c>Find</c> and have a parameter
    /// ending with <c>id</c>.
    /// </para>
    /// </remarks>
    /// <example><c>FindReservation(int reservationId)</c></example>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
    public static void Find(
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix)]
        [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
        object id)
    {
    }
    
    /// <summary>
    /// A convention for getting a list of all sub entities from an entity.
    /// </summary>
    /// <remarks>
    /// This convention may only produce a <c>200 Ok</c>, a <c>204 No content</c>,
    /// a <c>404 Not found</c> or a <c>500 internal error</c> response.
    /// <para>
    /// To apply this convention the method must start with <c>FindAll</c> and have a parameter
    /// ending with <c>id</c>.
    /// </para>
    /// </remarks>
    /// <example><c>FindAllReservationDetails(int reservationId)</c></example>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
    public static void FindAll(
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix)]
        [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
        object id)
    {
    }

    #endregion

    #region POST

    /// <summary>
    /// A convention for creating a new entity.
    /// </summary>
    /// <remarks>
    /// This convention may only produce a <c>201 Created</c>, a <c>400 Bad request</c>,
    /// or a <c>500 internal error</c> response.
    /// <para>
    /// To apply this convention the method must start with <c>Create</c> and have a parameter
    /// representing the new object. The name of the parameter is not matched.
    /// </para>
    /// </remarks>
    /// <example><c>CreateReservation(CreateReservationDTO reservation)</c></example>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
    public static void Create(
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)]
        [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
        object model)
    {
    }

    #endregion

    #region PUT
    
    /// <summary>
    /// A convention for updating an entity.
    /// </summary>
    /// <remarks>
    /// This convention may only produce a <c>204 No content</c>,  a <c>400 Bad request</c>,
    /// a <c>404 Not Found</c>, or a <c>500 internal error</c> response.
    /// <para>
    /// To apply this convention the method must start with <c>Update</c> and have two parameters.
    /// The first must end with <c>id</c> and the second is the new value, the name is not matched.
    /// </para>
    /// </remarks>
    /// <example><c>UpdateReservation(int id, UpdateReservationDTO reservation)</c></example>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
    public static void Update(
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix)]
        [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
        object id,
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)]
        [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
        object model)
    {
    }

    #endregion

    #region DELETE

    /// <summary>
    /// A convention for deleting an entity.
    /// </summary>
    /// <remarks>
    /// This convention may only produce a <c>204 No content</c> or a <c>500 internal error</c>
    /// response.
    /// <para>
    /// To apply this convention the method must start with <c>Delete</c> and have a parameter
    /// which ends with <c>id</c>.
    /// </para>
    /// </remarks>
    /// <example><c>DeleteReservation(int id)</c></example>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
    public static void Delete(
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix)]
        [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
        object id)
    {
    }

    #endregion
}