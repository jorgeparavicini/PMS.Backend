// -----------------------------------------------------------------------
// <copyright file="ReservationProfile.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using AutoMapper;
using PMS.Backend.Core.Entities.Reservation;
using PMS.Backend.Features.GraphQL.Reservation.Models;

namespace PMS.Backend.Features.GraphQL.Reservation;

public class ReservationProfile : Profile
{
    public ReservationProfile()
    {
        CreateMap<GroupReservation, GroupReservationDTO>();
    }
}
