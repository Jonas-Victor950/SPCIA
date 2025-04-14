using System.ComponentModel.DataAnnotations;

namespace BRCSystem.ClassLibrary.EnrollmentSystem.Entities
{
    public class Supplier
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Active cannot be null!")]
        public bool Active { get; set; }
        [Required(ErrorMessage = "Name cannot be null or empty!")]
        public string Name { get; set; }
        public virtual List<Material>? Materials { get; set; }
    }
}