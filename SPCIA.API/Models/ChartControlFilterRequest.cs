using BRCSystem.ClassLibrary.SPCIA.Entities;
using System.ComponentModel.DataAnnotations;

namespace SPCIA.API.Models
{
    public class ChartControlFilterRequest
    {
        [Required(ErrorMessage = "MachineId cannot be null or empty!")]
        public int MachineId { get; set; }

        [Required(ErrorMessage = "ProcessId cannot be null or empty!")]
        public int ProcessId { get; set; }

        [Required(ErrorMessage = "CharacteristicId cannot be null or empty!")]
        public int CharacteristicId { get; set; }

        public int? SampleSize { get; set; }
        public List<Classifier>? Classifiers { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
