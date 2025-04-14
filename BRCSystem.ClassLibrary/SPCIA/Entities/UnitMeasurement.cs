using System.ComponentModel.DataAnnotations;

namespace BRCSystem.ClassLibrary.SPCIA.Entities
{
    public class UnitMeasurement
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Measurement cannot be null or empty!", AllowEmptyStrings = false)]
        public string Measurement { get; set; }

        [Required(ErrorMessage = "Symbol cannot be null or empty!", AllowEmptyStrings = false), MaxLength(8, ErrorMessage = "Symbol cannot have more than 8 characters")]
        public string Symbol { get; set; }

        public bool Active { get; set; } = true;

    }
}
