using System.ComponentModel.DataAnnotations;

namespace BRCSystem.ClassLibrary.SPCIA.Entities
{
    public class Characteristic
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Name cannot be null or empty!", AllowEmptyStrings = false)]
        public required string Name { get; set; }

        [Required(ErrorMessage = "UnitMeasurementId cannot be null!")]
        public required int UnitMeasurementId { get; set; }
        public virtual UnitMeasurement? UnitMeasurement { get; set; }

        [Required(ErrorMessage = "USL cannot be null!")]
        public required double USL { get; set; }

        [Required(ErrorMessage = "LSL cannot be null!")]
        public required double LSL { get; set; }
    }
}
