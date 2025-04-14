using BRCSystem.ClassLibrary.EnrollmentSystem.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BRCSystem.ClassLibrary.SPCIA.Entities
{
    public class MachineProfile
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Name cannot be null or empty!", AllowEmptyStrings = true)]
        public required string Name { get; set; }

        public virtual List<MachineProfileParameter>? MachineProfileParameters { get; set; }

        public virtual List<Machine>? Machines { get; set; }

        public bool Active { get; set; }
    }
}
