using BRCSystem.ClassLibrary.Authentication.Entities;
using BRCSystem.ClassLibrary.EnrollmentSystem.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace BRCSystem.ClassLibrary.POSStation.Entities
{
    public class LotStepData
    {
        public int? Id { get; set; }

        public StatusLotStepData StatusLotStepData { get; set; } = StatusLotStepData.Unstarted;

        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }

        public int? OperadorInId { get; set; }
        public User? OperadorIn { get; set; }

        public int? OperadorOutId { get; set; }
        public User? OperadorOut { get; set; }

        public int? QtyIn { get; set; }

        public int? QtyOut { get; set; }

        public List<LotStepDataMaterial>? LotStepDataMaterials { get; set; }
        public bool MaterialHasAnyDamage { get; set; } = false;

        public string? Remark { get; set; }

        public int ProductStepId { get; set; }
        public ProductStep? ProductStep { get; set; }

        public int? ProductLotId { get; set; }
        public ProductLot? ProductLot { get; set; }

        public List<RejectInformation>? RejectInformations { get; set; }

        public int? MachineId { get; set; }
        public Machine? Machine { get; set; }

        public int? StartYieldId { get; set; }
        public LotStepData? StartYield { get; set; }

        [NotMapped]
        public bool? IsSplitted { get; set; }
        [NotMapped]
        public List<string>? NewLotsSplit { get; set; }
        [NotMapped]
        public List<CapacitiesRequest>? CapacitiesRequest { get; set; }
        [NotMapped]
        public bool Hascutting { get; set; }
        [NotMapped]
        public bool HasStrip { get; set; }
        [NotMapped]
        public int? Column { get; set; }
        [NotMapped]
        public int? Row { get; set; }

        [NotMapped]
        public List<StripRejection>? stripRejections { get; set; }
        [NotMapped]
        public double? StationYield
        {
            get
            {
                return QtyIn != null && QtyOut != null ? Math.Round((double)QtyOut! / (double)QtyIn! * 100D, 2) : null;
            }
        }
    }

    public class CapacitiesRequest
    {
        public int CapacityId { get; set; }
        public int Qty { get; set; }
    }
}