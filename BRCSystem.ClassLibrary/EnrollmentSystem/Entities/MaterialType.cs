using BRCSystem.ClassLibrary.POSStation.Entities;
using System.ComponentModel.DataAnnotations;

namespace BRCSystem.ClassLibrary.EnrollmentSystem.Entities
{
    public class MaterialType
    {
        public int? Id { get; set; }
        public bool Active { get; set; } = true;
        [Required(ErrorMessage = "Type cannot be null or empty!", AllowEmptyStrings = false)]
        public string Type { get; set; }
        [Required(ErrorMessage = "Category cannot be null")]
        public virtual Category Category { get; set; }
        public virtual List<Material>? Materials { get; set; }
        public virtual List<Station>? Stations { get; set; }

        [Required(ErrorMessage = "HasStrip cannot be null!")]
        public bool HasStrip { get; set; } = false;
    }
}