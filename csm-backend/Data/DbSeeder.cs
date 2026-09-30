using csm_backend.Configs;
using csm_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace csm_backend.Data
{
    public class DbSeeder
    {
        public static async Task SeedAdminAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            await db.Database.MigrateAsync();

            string hasedAdminPassword = BCrypt.Net.BCrypt.HashPassword(AdminConfig.AdminPassword);
            var adminUser = await db.Users.FirstOrDefaultAsync(u => u.Username == AdminConfig.AdminUsername);

            if (adminUser == null)
            {
                var newAdmin = new UserModel
                {
                    Username = AdminConfig.AdminUsername,
                    Password = hasedAdminPassword,
                    UserRole = UserRole.Admin,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                db.Users.Add(newAdmin);
                await db.SaveChangesAsync();
                Console.WriteLine($"[Seeder] Create admin user successfully");
            }
            else
            {
                adminUser.Password = hasedAdminPassword;
                adminUser.UserRole = UserRole.Admin;
                adminUser.UpdatedAt = DateTime.UtcNow;
                
                db.Users.Update(adminUser);
                await db.SaveChangesAsync();
                Console.WriteLine($"[Seeder] Updated existing Admin User: '{AdminConfig.AdminUsername}' password and role.");
            }
        }
    }
}