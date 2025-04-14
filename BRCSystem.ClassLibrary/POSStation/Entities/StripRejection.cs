using System.ComponentModel.DataAnnotations;

namespace BRCSystem.ClassLibrary.POSStation.Entities
{
    public class StripRejection
    {
        public int? Id { get; set; }

        [Range(1, int.MaxValue)]
        public int Position { get; set; }

        [Range(1, int.MaxValue)]
        public int Line { get; set; }

        [Range(1, int.MaxValue)]
        public int Column { get; set; }

        public StatusStripRejection? statusStripRejection { get; set; }

        [Required(ErrorMessage = "LotStepDataId cannot be null")]
        public int LotStepDataId { get; set; }
        public LotStepData? LotStepData { get; set; }
        public bool Active { get; set; } = true;

    }

    public enum StatusStripRejection
    {
        RW,
        R,
        X
    }
}