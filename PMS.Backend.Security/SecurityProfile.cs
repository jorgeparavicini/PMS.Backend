using AutoMapper;
using PMS.Backend.Security.Entity;

namespace PMS.Backend.Security
{
    public class SecurityProfile: Profile
    {
        public SecurityProfile()
        {
            CreateMap<User, Models.User>();
        }
    }
}
