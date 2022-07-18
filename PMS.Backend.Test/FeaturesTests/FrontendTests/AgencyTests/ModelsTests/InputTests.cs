﻿using PMS.Backend.Core.Entities.Agency;
using PMS.Backend.Features.Frontend.Agency.Models.Input;
using PMS.Backend.Features.Frontend.Agency.Models.Input.Validation;

namespace PMS.Backend.Test.FeaturesTests.FrontendTests.AgencyTests.ModelsTests;

public class InputTests
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
