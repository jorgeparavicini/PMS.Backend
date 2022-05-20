using Microsoft.AspNetCore.Identity;

namespace PMS.Backend.Common
{
    public static class Roles
    {
        public static IEnumerable<IdentityRole> All => new[] { AdminRole };

        public static IdentityRole AdminRole { get; } = new("admin") { NormalizedName = "ADMIN" };
    }
}
