using AutoMapper;

namespace PMS.Backend.Features.Agency.Mappings;

public class AgencyContactMappings : Profile
{
    public AgencyContactMappings()
    {
        CreateMap<Entities.AgencyContact, Models.AgencyContact>()
            .ForMember(contact => contact.Email, opt => opt.MapFrom(src => src.ContactDetails.Email))
            .ForMember(contact => contact.Phone, opt => opt.MapFrom(src => src.ContactDetails.Phone))
            .ForMember(contact => contact.Street, opt => opt.MapFrom(src => src.Address.Street))
            .ForMember(contact => contact.City, opt => opt.MapFrom(src => src.Address.City))
            .ForMember(contact => contact.State, opt => opt.MapFrom(src => src.Address.State))
            .ForMember(contact => contact.Country, opt => opt.MapFrom(src => src.Address.Country))
            .ForMember(contact => contact.ZipCode, opt => opt.MapFrom(src => src.Address.ZipCode))
            .ForAllMembers(member => member.ExplicitExpansion());
    }
}
