namespace csm_backend.Configs
{
    public class JwtConfig
    {
        public static string JwtSecret => Environment.GetEnvironmentVariable("JWT_SECRET") ?? "";
        public static string JwtIssuer => Environment.GetEnvironmentVariable("JWT_ISSUER") ?? "";
        public static string JwtAudience => Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? "";
    }
}