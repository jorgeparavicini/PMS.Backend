namespace PMS.Backend.Features.Authentication.Models
{
    public record LoginModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
