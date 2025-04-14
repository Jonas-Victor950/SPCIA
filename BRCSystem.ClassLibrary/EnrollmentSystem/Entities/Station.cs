using BRCSystem.ClassLibrary.Authentication.Entities;
using BRCSystem.ClassLibrary.POSStation.Entities;
using System.ComponentModel.DataAnnotations;

namespace BRCSystem.ClassLibrary.EnrollmentSystem.Entities
{
    public class Station
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Name cannot be null or empty!", AllowEmptyStrings = false)]
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool Active { get; set; } = true;
        [Required(ErrorMessage = "Operatos cannot be null")]
        public virtual List<User?>? Operators { get; set; } = new List<User>();
        public virtual List<MaterialType?>? MaterialTypes { get; set; } = new List<MaterialType>();
        public List<Rejection>? Rejections { get; set; }
        public Room? Room { get; set; }
        public bool HasSplit { get; set; } = false;
        public bool IsPacking { get; set; } = false;
        public bool Hascutting { get; set; } = false;
        public bool CanBeConditional { get; set; } = false;
        public bool IsTapeReel { get; set; } = false;
        public bool IsShipping { get; set; } = false;
        public bool IsQA { get; set; } = false;
        public bool IsYieldStation { get; set; } = false;
        public virtual List<Machine>? Machines { get; set; } = new List<Machine>();
    }
}