using BRCSystem.ClassLibrary.POSStation.Entities;
using System.ComponentModel.DataAnnotations;

namespace BRCSystem.ClassLibrary.EnrollmentSystem.Entities
{
    public class Material
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Active cannot be null!")]
        public bool Active { get; set; }
        [Required(ErrorMessage = "PartNumber cannot be null or empty!", AllowEmptyStrings = false)]
        public string PartNumber { get; set; }

        [Required(ErrorMessage = "SupplierPn cannot be null or empty!", AllowEmptyStrings = false)]
        public string SupplierPn { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "MaterialTypeId cannot be null!")]
        public int MaterialTypeId { get; set; }
        public virtual MaterialType? MaterialType { get; set; }
        [Required(ErrorMessage = "SupplierId cannot be null!")]
        public int SupplierId { get; set; }
        public virtual Supplier? Supplier { get; set; }
        public virtual List<ProductStep>? ProductSteps { get; set; }

        [Range(1, int.MaxValue)]
        public int? StripRows { get; set; }
        [Range(1, int.MaxValue)]
        public int? StripColumns { get; set; }
    }
}
