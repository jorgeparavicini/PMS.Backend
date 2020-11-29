namespace PMS.Backend.Service.Configuration
{
    // Instantiated Implicitly
    // ReSharper disable once ClassNeverInstantiated.Global
    public record JwtConfiguration
    {
        public string Issuer { get; init; } = null!;

        public string Audience { get; init; } = null!;

        public string Secret { get; init; } = null!;

        public int AccessTokenExpiration { get; init; }

        public int RefreshTokenExpiration { get; init; }
    }
}
