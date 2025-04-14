using System.ComponentModel.DataAnnotations;

namespace BRCSystem.ClassLibrary.EnrollmentSystem.Entities
{
    public class ProgramEntity
    {
        public int? Id { get; set; }
        public bool Active { get; set; }
        [Required(ErrorMessage = "Name cannot be null or empty!", AllowEmptyStrings = false)]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "StationId cannot be null!")]
        public int StationId { get; set; }
        public virtual Station? Station { get; set; }
    }
}