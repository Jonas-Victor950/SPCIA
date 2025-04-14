

namespace BRCSystem.ClassLibrary.Authentication.Entities
{
    public class Parameters
    {
        public int? Id { get; set; }

        public String EmailDomain { get; set; }

        public int EmailPort { get; set; }

        public String EmailAccount { get; set; }

        public String EmailPassword { get; set; }

        public int TokenExpireTimeMinutes { get; set; } = 480;
        public bool AbleMFA { get; set; } = false;
    }
}