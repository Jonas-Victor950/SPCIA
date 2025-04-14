using BRCSystem.ClassLibrary.EnrollmentSystem.Entities;
using System.ComponentModel.DataAnnotations;

namespace BRCSystem.ClassLibrary.POSStation.Entities
{
    public class Capacity
    {
        public int? Id { get; set; }

        public bool Active { get; set; } = true;
        [Required(ErrorMessage = "CapClass cannot be null ou empty", AllowEmptyStrings = false)]
        public string CapClass { get; set; }
        [Required(ErrorMessage = "ProductId cannot be null ou empty")]
        public int? ProductId { get; set; }
        public virtual Product? Product { get; set; }
        [Required(ErrorMessage = "ProductReferencyId cannot be null ou empty")]
        public int ProductReferencyId { get; set; }
        public virtual Product? ProductReferency { get; set; }
    }
}