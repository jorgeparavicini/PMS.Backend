﻿// -----------------------------------------------------------------------
// <copyright file="AgencyProfile.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using AutoMapper;
using PMS.Backend.Core.Entities.Agency;
using PMS.Backend.Features.GraphQL.Agency.Models.Payload;

namespace PMS.Backend.Features.GraphQL.Agency;

/// <summary>
///     Mapping profile for all <see cref="Core.Entities.Agency.Agency"/> related inputs and payloads.
/// </summary>
public class AgencyProfile : Profile
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="AgencyProfile"/> class.
    /// </summary>
    public AgencyProfile()
    {
        CreateMap<Core.Entities.Agency.Agency, AgencyPayload>();
        CreateMap<AgencyContact, AgencyContactPayload>();
    }
}
