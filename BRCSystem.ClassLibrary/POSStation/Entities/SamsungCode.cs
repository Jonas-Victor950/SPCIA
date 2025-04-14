using System.ComponentModel.DataAnnotations;

namespace BRCSystem.ClassLibrary.POSStation.Entities
{
    public class SamsungCode
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "SecCode cannot be null")]
        public string SecCode { get; set; }

        //Referente a ProductCode
        [Required(ErrorMessage = "MultCode cannot be null")]
        public string MultCode { get; set; }

        [Required(ErrorMessage = "MultRetorna cannot be null")]
        public string MultRetorna { get; set; }
        public string Specification { get; set; }
    }
}