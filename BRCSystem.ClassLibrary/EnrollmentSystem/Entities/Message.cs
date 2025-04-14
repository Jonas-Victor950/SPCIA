using BRCSystem.ClassLibrary.Authentication.Entities;
using System.ComponentModel.DataAnnotations;

namespace BRCSystem.ClassLibrary.EnrollmentSystem.Entities
{
    public class Message
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "DateTime cannot be null!")]
        public required DateTime DateTime { get; set; }

        [Required(ErrorMessage = "MessageText cannot be null or empty!", AllowEmptyStrings = false)]
        public required string MessageText { get; set; }

        public required int UserId { get; set; }
        public User? User { get; set; }

        public bool Read { get; set; } = false;
    }
}
