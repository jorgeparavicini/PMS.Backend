using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Detached.Annotations;
using Microsoft.EntityFrameworkCore;
using PMS.Backend.Common.Models;

namespace PMS.Backend.Core.Entities.Agency;

/// <summary>
/// An agency through which guests can make reservations.
/// </summary>
public class Agency : Entity
{
    /// <summary>
    /// The name of this entity as a business object.
    /// </summary>
    /// <remarks>This is used to define the endpoint and the odata metadata.</remarks>
    public const string BusinessObjectName = "Agencies";

    #region Properties

    /// <summary>
    /// The legal name of the agency.
    /// </summary>
    [MinLength(1)]
    [MaxLength(255)]
    public string LegalName { get; set; } = null!;

    /// <summary>
    /// The default commission rate for agents in this agency as a fraction.
    /// <para>
    /// If the agent has not specified a commission rate, this default rate will be used.
    /// </para>
    /// </summary>
    /// <remarks>
    /// Max value is 9.9999 but semantically it does not make sense to have it more than 1.0.
    /// <para>Min value is 0.0000.</para>
    /// </remarks>
    [Precision(5, 4)]
    public decimal? DefaultCommissionRate { get; set; }

    /// <summary>
    /// The default commission rate for extra goods provided for agents in this agency as a
    /// fraction.
    /// <para>
    /// If the agent has not specified a commission rate for extra good, this default rate will be
    /// used.
    /// </para>
    /// </summary>
    /// <remarks>
    /// Max value is 9.9999 but semantically it does not make sense to have it more than 1.0.
    /// <para>Min value is 0.0000.</para>
    /// </remarks>
    [Precision(5, 4)]
    public decimal? DefaultCommissionOnExtras { get; set; }

    /// <summary>
    /// The method to be used for commissions.
    /// </summary>
    [DefaultValue(CommissionMethod.DeductedByAgency)]
    public CommissionMethod CommissionMethod { get; set; }

    /// <summary>
    /// An optional phone number in case of emergencies.
    /// </summary>
    [MaxLength(255)]
    [Phone]
    public string? EmergencyPhone { get; set; }

    /// <summary>
    /// An optional email address in case of emergencies.
    /// </summary>
    [MaxLength(255)]
    [EmailAddress]
    public string? EmergencyEmail { get; set; }

    #endregion

    #region Relations

    /// <summary>
    /// The agents known that work for this agency.
    /// </summary>
    /// <remarks>
    /// At least one contact is required as relations are established through the contact
    /// and not the agency.
    /// </remarks>
    [Composition]
    [SuppressMessage("ReSharper", "CollectionNeverQueried.Global")]
    public IList<AgencyContact> AgencyContacts { get; set; } = new List<AgencyContact>();

    #endregion

    // TODO: Add Company, Association, Default Channel, Default Origination
}
