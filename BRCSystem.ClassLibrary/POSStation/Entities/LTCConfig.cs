using System.ComponentModel.DataAnnotations;

namespace BRCSystem.ClassLibrary.POSStation.Entities
{
    public class LTCConfig
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "EmissionDate cannot be null")]
        public DateTime EmissionDate { get; set; }

        [Required(ErrorMessage = "PreparatedBy cannot be null")]
        public string PreparatedBy { get; set; }

        [Required(ErrorMessage = "RevisionNumber cannot be null")]
        public int RevisionNumber { get; set; }

        [Required(ErrorMessage = "RevisionDate cannot be null")]
        public DateTime RevisionDate { get; set; }

        [Required(ErrorMessage = "ApprovedBy cannot be null")]
        public string ApprovedBy { get; set; }
    }
}
