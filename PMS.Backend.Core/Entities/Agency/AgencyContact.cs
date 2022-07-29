using System.ComponentModel.DataAnnotations;
using PMS.Backend.Core.Entities.Reservation;

namespace PMS.Backend.Core.Entities.Agency;

/// <summary>
/// The contact of an agent part of an agency.
/// </summary>
public class AgencyContact : Entity
{
    #region Properties

    /// <summary>
    /// The full name of the contact.
    /// </summary>
    [MinLength(1)]
    [MaxLength(255)]
    public string ContactName { get; set; } = null!;

    /// <summary>
    /// An optional email address for the contact.
    /// </summary>
    [MaxLength(255)]
    [EmailAddress]
    public string? Email { get; set; }

    /// <summary>
    /// An optional phone number for the contact.
    /// </summary>
    [MaxLength(255)]
    [Phone]
    public string? Phone { get; set; }

    /// <summary>
    /// An optional address for the contact.
    /// </summary>
    [MaxLength(255)]
    public string? Address { get; set; }

    /// <summary>
    /// An optional city of residence for the contact.
    /// </summary>
    [MaxLength(255)]
    public string? City { get; set; }

    /// <summary>
    /// An optional ZipCode where the contact resides.
    /// </summary>
    [MaxLength(255)]
    public string? ZipCode { get; set; }

    /// <summary>
    /// Is the contact an agent that provides frequent sells.
    /// </summary>
    public bool IsFrequentVendor { get; set; }

    #endregion

    #region Relations

    /// <summary>
    /// The id of the associated agency.
    /// </summary>
    /// <remarks>
    /// This is an EF-Core relation, hence both the Id and the agency are required.
    /// </remarks>
    /// <seealso cref="Agency"/>
    public int AgencyId { get; set; }

    /// <summary>
    /// The associated agency.
    /// </summary>
    /// <remarks>
    /// This is an EF-Core relation, hence both the Id and the agency are required.
    /// </remarks>
    /// <seealso cref="AgencyId"/>
    public Agency Agency { get; set; } = null!;

    /// <summary>
    /// A list of all reservations this contact is responsible for.
    /// </summary>
    public IList<GroupReservation> GroupReservations { get; } = new List<GroupReservation>();

    #endregion

    // TODO: Add Country of residence, Language
}
