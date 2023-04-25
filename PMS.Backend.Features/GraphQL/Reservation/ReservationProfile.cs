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

/// <summary>
///     The automapper profile for the reservation feature.
/// </summary>
public class ReservationProfile : Profile
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ReservationProfile" /> class.
    /// </summary>
    public ReservationProfile()
    {
        CreateMap<GroupReservation, GroupReservationDTO>();
    }
}
