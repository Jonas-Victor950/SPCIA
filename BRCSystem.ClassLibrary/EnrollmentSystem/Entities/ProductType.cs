using System.ComponentModel.DataAnnotations;

namespace BRCSystem.ClassLibrary.EnrollmentSystem.Entities
{
    public class ProductType
    {
        public int? Id { get; set; }
        public bool Active { get; set; } = true;
        [Required(ErrorMessage = "Type cannot be null or empty!", AllowEmptyStrings = false)]
        public string Type { get; set; }
        [Required(ErrorMessage = "Prefix cannot be null or empty!", AllowEmptyStrings = false)]
        public string Prefix { get; set; }
    }
}