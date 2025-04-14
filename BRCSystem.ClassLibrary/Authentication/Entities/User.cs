using BRCSystem.ClassLibrary.EnrollmentSystem.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BRCSystem.ClassLibrary.Authentication.Entities
{
    public class User
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Fist Name is required")]
        public string FirstName { get; set; }
        public string? EmploeeId { get; set; }
        public string? Tel { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
        public Role Role { get; set; }
        public bool Active { get; set; }
        [JsonIgnore]
        public virtual string PasswordHash { get; set; }
        public virtual List<Station>? Stations { get; set; }
        public string? Email { get; set; }
        public bool Able { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? LastUpdate { get; set; }
        public string? UpdatedBy { get; set; }
        [JsonIgnore]
        public string? MFACode { get; set; }
        public DateTime? MFATime { get; set; }
    }
}
