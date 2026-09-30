namespace csm_backend.Configs
{
    public class AdminConfig
    {
        public static string AdminUsername => Environment.GetEnvironmentVariable("ADMIN_USERNAME") ?? "";
        public static string AdminPassword => Environment.GetEnvironmentVariable("ADMIN_PASSWORD") ?? "";
    }
}