namespace TransPoster.Application;

public class AppConfiguration
{
    public string Secret { get; set; } = null!;
    public int TokenValidityInSeconds { get; set; }
    public int RefreshTokenExpiryTimeInMinutes { get; set; }
}