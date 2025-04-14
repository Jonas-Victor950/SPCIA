using System.ComponentModel.DataAnnotations;

namespace BRCSystem.ClassLibrary.POSStation.Entities
{
    public class Rejection
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Description cannot be null ou empty", AllowEmptyStrings = false)]
        public string Description { get; set; }
        public bool Active { get; set; }
    }
}