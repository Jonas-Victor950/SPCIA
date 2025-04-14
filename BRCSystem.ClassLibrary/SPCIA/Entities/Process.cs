using BRCSystem.ClassLibrary.EnrollmentSystem.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BRCSystem.ClassLibrary.SPCIA.Entities
{
    public class Process
    {
        public int? Id { get; set; }

        public List<Characteristic> Characteristics { get; set; }

        public int StationId { get; set; }

        public virtual Station? Station { get; set; }

        public int SampleSize { get; set; }

        [Required(ErrorMessage = "Process Name cannot be null or empty!", AllowEmptyStrings = false)]
        public string ProcessName { get; set; }

        public virtual List<Product>? Products { get; set; }

        public bool Active { get; set; } = true;

        [NotMapped]
        public bool? AbleUpdate { get; set; }
    }
}
