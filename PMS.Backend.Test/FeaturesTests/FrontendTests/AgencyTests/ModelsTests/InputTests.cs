using PMS.Backend.Core.Entities.Agency;
using PMS.Backend.Features.Frontend.Agency.Models.Input;
using PMS.Backend.Features.Frontend.Agency.Models.Input.Validation;

namespace PMS.Backend.Test.FeaturesTests.FrontendTests.AgencyTests.ModelsTests;

public class InputTests
{
    [Fact]
    public void CreateAgencyContactDTO_ShouldMatchEFModelConfiguration()
    {
        TestExtensions
            .AssertEqualValidation<
                Agency,
                CreateAgencyDTO,
                CreateAgencyDTOValidator>();
    }
}
