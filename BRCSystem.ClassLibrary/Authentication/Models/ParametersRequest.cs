

namespace BRCSystem.ClassLibrary.Authentication.Models
{
    public class ParametersRequest
    {
        public int? Id { get; set; }
        public string EmailDomain { get; set; }
        public int EmailPort { get; set; }
        public string EmailAccount { get; set; }
        public string? EmailPassword { get; set; }
        public int AutoLogoutTimeMinutes { get; set; } = 20;
        public bool AbleMFA { get; set; }

    }
}