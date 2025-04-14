using System.ComponentModel.DataAnnotations;

namespace BRCSystem.ClassLibrary.POSStation.Entities
{
    public class ImageLabel
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Description cannot be null or empty", AllowEmptyStrings = false)]
        public string Description { get; set; }
        [Required(ErrorMessage = "Top cannot be null or empty", AllowEmptyStrings = false)]
        public int Top { get; set; }
        [Required(ErrorMessage = "Left cannot be null or empty", AllowEmptyStrings = false)]
        public int Left { get; set; }
        public string BackGroundColor { get; set; }
        public string Color { get; set; }
        public int FontSize { get; set; }
        public int FontWeight { get; set; }
        public string Align { get; set; }
    }
}