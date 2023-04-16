// -----------------------------------------------------------------------
// <copyright file="AgencyProfile.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using AutoMapper;
using PMS.Backend.Core.Entities.Agency;
using PMS.Backend.Features.Features.Agency.Models;

namespace PMS.Backend.Features.Features.Agency;

public class AgencyProfile : Profile
{
    public AgencyProfile()
    {
        CreateMap<Core.Entities.Agency.Agency, AgencyDTO>();
        CreateMap<AgencyContact, AgencyContactDTO>();
    }
}
