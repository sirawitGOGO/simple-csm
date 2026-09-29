namespace csm_backend.Configs
{
    public class DbConfig
    {
        public static string DbHost { get; set; } = Environment.GetEnvironmentVariable("DB_HOST") ?? "";
        public static string DbName => Environment.GetEnvironmentVariable("DB_Name") ?? "";
        public static string DbUsername => Environment.GetEnvironmentVariable("DB_USERNAME") ?? "";
        public static string DbPassword => Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "";

        public static string DbConnectionString => $"Host={DbHost}; Database={DbName}; Username={DbUsername}; Password={DbPassword}; SSL Mode=VerifyFull; Channel Binding=Require;";
    }
}