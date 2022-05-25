﻿using PMS.Backend.Features.Frontend.Agency.Models;
using PMS.Backend.Features.Frontend.Agency.Models.Output;

namespace PMS.Backend.Features.Frontend.Agency.Services.Contracts;

public interface IAgencyService
{
    Task<IEnumerable<AgencyDTO>> GetAllAgenciesAsync();
    
    Task<AgencyDTO?> FindAgencyAsync(int id);
}