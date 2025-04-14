using BRCSystem.ClassLibrary.Authentication.Entities;

namespace BRCSystem.ClassLibrary.POSStation.Entities
{
    public class PasswordHistory
    {
        public int? Id { get; set; }

        public string Password { get; set; }

        public int userId { get; set; }
        public User User { get; set; }
    }
}