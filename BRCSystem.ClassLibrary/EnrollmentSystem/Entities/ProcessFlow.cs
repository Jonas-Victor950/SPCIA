using BRCSystem.ClassLibrary.POSStation.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BRCSystem.ClassLibrary.EnrollmentSystem.Entities
{
    public class ProcessFlow
    {
        public int? Id { get; set; }
        public bool Active { get; set; }
        [Required(ErrorMessage = "Name cannot be null or empty!", AllowEmptyStrings = false)]
        public string Name { get; set; }
        [Required(ErrorMessage = "ProductTypeId cannot be null!")]
        public int ProductTypeId { get; set; }
        public virtual ProductType? ProductType { get; set; }
        public virtual List<ProcessStep> ProcessSteps { get; set; } = new List<ProcessStep>();
        [NotMapped]
        public bool? AbleUpdate { get; set; } = true;
    }
}