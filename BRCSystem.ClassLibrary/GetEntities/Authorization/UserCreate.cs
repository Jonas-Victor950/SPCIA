using BRCSystem.ClassLibrary.Authentication.Entities;

namespace BRCSystem.ClassLibrary.GetEntities.Authorization
{
    public static class UserCreate
    {
        public static User GetUser()
        {
            Random rand = new();
            return new User
            {
                Id = null,
                Able = true,
                Active = true,
                Email = "admin@email.com",
                EmploeeId = rand.Next(999999).ToString(),
                FirstName = "Admin",
                PasswordHash = "admin",
                Role = Role.ADMINISTRATOR,
                Tel = rand.Next(999999999).ToString(),
                Username = "admin"
            };
        }

        public static User GetSystemUser()
        {
            Random rand = new();
            return new User
            {
                Id = null,
                Able = false,
                Active = true,
                Email = "system@multilaser.com.br",
                EmploeeId = rand.Next(999999).ToString(),
                FirstName = "system",
                PasswordHash = "system",
                Role = Role.SYSTEM,
                Tel = rand.Next(999999999).ToString(),
                Username = "system"
            };
        }
    }
}