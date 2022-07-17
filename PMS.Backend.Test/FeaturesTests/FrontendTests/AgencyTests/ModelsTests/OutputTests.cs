using PMS.Backend.Core.Entities.Agency;
using PMS.Backend.Features.Frontend.Agency.Models.Output;
using PMS.Backend.Features.Frontend.Agency.Models.Output.Validation;

namespace PMS.Backend.Test.FeaturesTests.FrontendTests.AgencyTests.ModelsTests;

public class OutputTests
{
    [Fact]
    public void AgencyContactDTO_ShouldMatchEFModelConfiguration()
    {
        TestExtensions
            .AssertEqualValidation<AgencyContact, AgencyContactDTO, AgencyContactDTOValidator>(
                true);
    }

    [Fact]
    public void AgencyDetailDTO_ShouldMatchEFModelConfiguration()
    {
        TestExtensions
            .AssertEqualValidation<Agency, AgencyDetailDTO, AgencyDetailDTOValidator>(true);
    }

    [Fact]
    public void AgencySummaryDTO_ShouldMatchEFModelConfiguration()
    {
        TestExtensions
            .AssertEqualValidation<Agency, AgencySummaryDTO, AgencySummaryDTOValidator>(true);
    }
}
