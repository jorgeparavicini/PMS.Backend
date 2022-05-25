using PMS.Backend.Core.Entities;
using PMS.Backend.Features.Frontend.Agency.Models.Input;
using PMS.Backend.Features.Frontend.Agency.Models.Output;

namespace PMS.Backend.Features.Frontend.Agency;

public class Profile : AutoMapper.Profile
{
    public Profile()
    {
        CreateMap<Core.Entities.Agency, AgencyDTO>();
        CreateMap<AgencyInputDTO, Core.Entities.Agency>()
            .ConstructUsing(x => 
                new Core.Entities.Agency(
                    x.LegalName,
                    x.CommissionMethod,
                    x.DefaultCommissionRate,
                    x.DefaultCommissionOnExtras,
                    x.EmergencyPhone,
                    x.EmergencyEmail));
        CreateMap<AgencyContact, AgencyContactDTO>();
        CreateMap<AgencyContactInputDTO, AgencyContact>()
            .ConstructUsing(x =>
                new AgencyContact(
                    x.ContactName,
                    x.IsFrequentVendor,
                    0,
                    x.City,
                    x.ZipCode,
                    x.Address,
                    x.Phone,
                    x.Email));
    }
}