using PMS.Backend.Core.Entities.Agency;
using PMS.Backend.Features.Frontend.Agency.Models.Input;
using PMS.Backend.Features.Frontend.Agency.Models.Input.Validation;
using PMS.Backend.Test.Extensions;

namespace PMS.Backend.Test.FeaturesTests.FrontendTests.AgencyTests;

public class InputModelsTests
{
    [Fact]
    public void CreateAgencyDTO_ShouldMatchEFModelConfiguration()
    {
        TestExtensions.AssertEqualValidation<Agency, CreateAgencyDTO, CreateAgencyDTOValidator>();
    }

    [Fact]
    public void CreateAgencyContactDTO_ShouldMatchEFModelConfiguration()
    {
        TestExtensions
            .AssertEqualValidation<
                AgencyContact,
                CreateAgencyContactDTO,
                CreateAgencyContactDTOValidator>();
    }

    [Fact]
    public void UpdateAgencyDTO_ShouldMatchEFModelConfiguration()
    {
        TestExtensions.AssertEqualValidation<Agency, UpdateAgencyDTO, UpdateAgencyDTOValidator>();
    }

    [Fact]
    public void UpdateAgencyContactDTO_ShouldMatchEFModelConfiguration()
    {
        TestExtensions
            .AssertEqualValidation<
                AgencyContact,
                UpdateAgencyContactDTO,
                UpdateAgencyContactDTOValidator>();
    }
}
