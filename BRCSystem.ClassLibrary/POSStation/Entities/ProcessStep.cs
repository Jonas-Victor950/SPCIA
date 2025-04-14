using BRCSystem.ClassLibrary.EnrollmentSystem.Entities;
using System.ComponentModel.DataAnnotations;

namespace BRCSystem.ClassLibrary.POSStation.Entities
{
    public class ProcessStep
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Step cannot be null!"), Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int Step { get; set; }
        [Required(ErrorMessage = "StationId cannot be null!")]
        public int StationId { get; set; }
        public virtual Station? Station { get; set; }
    }
}