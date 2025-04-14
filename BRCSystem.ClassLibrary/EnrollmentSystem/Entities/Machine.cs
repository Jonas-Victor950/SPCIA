using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BRCSystem.ClassLibrary.EnrollmentSystem.Entities
{
    public class Machine
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Name cannot be null or empty!", AllowEmptyStrings = false)]
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool Active { get; set; } = true;
        public virtual List<Station>? Stations { get; set; }
        [NotMapped]
        public virtual List<int>? StationsIds { get; set; }
    }
}