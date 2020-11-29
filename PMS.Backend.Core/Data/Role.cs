using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace PMS.Backend.Core.Data
{
    public static class Role
    {
        public static IEnumerable<IdentityRole> Roles => new[] { AdminRole };

        public static IdentityRole AdminRole { get; } = new("admin") { NormalizedName = "ADMIN" };
    }
}
