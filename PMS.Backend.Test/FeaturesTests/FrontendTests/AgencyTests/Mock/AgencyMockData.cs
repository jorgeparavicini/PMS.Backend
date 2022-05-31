using PMS.Backend.Common.Models;
using PMS.Backend.Features.Frontend.Agency.Models.Input;
using PMS.Backend.Features.Frontend.Agency.Models.Output;

namespace PMS.Backend.Test.FeaturesTests.FrontendTests.AgencyTests.Mock;

public static class AgencyMockData
{
    public static List<AgencySummaryDTO> GetEmptyAgencies()
    {
        return new List<AgencySummaryDTO>();
    }

    public static List<AgencySummaryDTO> GetAgencySummaries()
    {
        return new List<AgencySummaryDTO>()
        {
            new(
                1,
                "Agency1",
                decimal.One,
                decimal.One,
                CommissionMethod.DeductedByAgency,
                null,
                null),
            new(2,
                "Agency2",
                decimal.Zero,
                decimal.Zero,
                CommissionMethod.DeductedByAgency,
                null,
                null)
        };
    }

    public static AgencyDetailDTO GetAgencyDetail()
    {
        return new AgencyDetailDTO(
            1,
            "Agency1",
            decimal.One,
            decimal.One,
            CommissionMethod.DeductedByAgency,
            null,
            null,
            new AgencyContactDTO[] { });
    }

    public static CreateAgencyDTO CreateAgencyDTO()
    {
        return new CreateAgencyDTO(
            CreatedAgencySummary().LegalName,
            CreatedAgencySummary().DefaultCommissionRate,
            CreatedAgencySummary().DefaultCommissionOnExtras,
            CreatedAgencySummary().CommissionMethod,
            CreatedAgencySummary().EmergencyPhone,
            CreatedAgencySummary().EmergencyEmail);
    }

    public static AgencySummaryDTO CreatedAgencySummary()
    {
        return new AgencySummaryDTO(
            1,
            "Agency1",
            decimal.One,
            decimal.One,
            CommissionMethod.DeductedByAgency,
            null,
            null);
    }

    public static UpdateAgencyDTO UpdateAgencyDTO()
    {
        return new UpdateAgencyDTO(
            UpdatedAgencySummary().Id,
            UpdatedAgencySummary().LegalName,
            UpdatedAgencySummary().DefaultCommissionRate,
            UpdatedAgencySummary().DefaultCommissionOnExtras,
            UpdatedAgencySummary().CommissionMethod,
            UpdatedAgencySummary().EmergencyPhone,
            UpdatedAgencySummary().EmergencyEmail);
    }

    public static AgencySummaryDTO UpdatedAgencySummary()
    {
        return CreatedAgencySummary();
    }
}