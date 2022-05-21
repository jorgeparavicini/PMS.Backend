namespace PMS.Backend.Features.Authentication.Models
{
    public record RegisterModel
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
