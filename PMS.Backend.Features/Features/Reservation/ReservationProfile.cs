// -----------------------------------------------------------------------
// <copyright file="ReservationProfile.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using AutoMapper;
using PMS.Backend.Core.Entities.Reservation;
using PMS.Backend.Features.Features.Reservation.Models;

namespace PMS.Backend.Features.Features.Reservation;

public class ReservationProfile : Profile
{
    public ReservationProfile()
    {
        CreateMap<GroupReservation, GroupReservationDTO>();
    }
}
