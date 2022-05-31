using PMS.Backend.Features.Frontend.Agency.Models.Input;
using PMS.Backend.Features.Frontend.Agency.Models.Output;

namespace PMS.Backend.Features.Frontend.Agency.Services.Contracts;

public interface IAgencyService
{
    #region Agency

    Task<IEnumerable<AgencySummaryDTO>> GetAllAgenciesAsync();
    
    Task<AgencyDetailDTO?> FindAgencyAsync(int id);

    Task<AgencySummaryDTO> CreateAgencyAsync(CreateAgencyDTO agency);

    Task<AgencySummaryDTO> UpdateAgencyAsync(UpdateAgencyDTO agency);

    Task DeleteAgencyAsync(int id);

    #endregion

    #region Agency Contact

    Task<IEnumerable<AgencyContactDTO>> GetAllContactsForAgencyAsync(int agencyId);
    
    Task<AgencyContactDTO?> FindContactForAgency(int agencyId, int contactId);

    Task<AgencyContactDTO> CreateContactForAgencyAsync(
        int agencyId,
        CreateAgencyContactDTO contact);

    Task<AgencyContactDTO> UpdateContactForAgencyAsync(
        int agencyId,
        UpdateAgencyContactDTO contact);

    Task DeleteAgencyContactAsync(int agencyId, int contactId);

    #endregion


}