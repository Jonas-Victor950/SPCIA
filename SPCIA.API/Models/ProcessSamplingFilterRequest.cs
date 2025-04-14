using BRCSystem.ClassLibrary.SPCIA.Entities;

namespace SPCIA.API.Models
{
    public class ProcessSamplingFilterRequest
    {
        public int? ProcessId { get; set; }
        public int? MachineId { get; set; }
        public int? OperatorId { get; set; }
        public int? ProductLotId { get; set; }
        public int? StationId { get; set; }
        public Classifier? Classifier { get; set; }
    }
}
