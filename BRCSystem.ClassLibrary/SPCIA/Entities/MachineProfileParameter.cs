using System.ComponentModel.DataAnnotations;

namespace BRCSystem.ClassLibrary.SPCIA.Entities
{
    public class MachineProfileParameter
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Name cannot be null or empty!", AllowEmptyStrings = false)]
        public required string Name { get; set; }

        public required int UnitMeasurementId { get; set; }
        public virtual UnitMeasurement? UnitMeasurement { get; set; }
    }
}