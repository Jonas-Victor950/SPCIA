using BRCSystem.ClassLibrary.Authentication.Entities;
using System.ComponentModel.DataAnnotations;

namespace BRCSystem.ClassLibrary.POSStation.Entities
{
    public class SWR
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Desc cannot be null!")]
        public string Desc { get; set; }


        public int? CreatedById { get; set; }
        public User? CreatedBy { get; set; }

        public DateTime? DateCreated { get; set; }

        public int ProductLotId { get; set; }
        public ProductLot? ProductLot { get; set; }

        public int? LotStepDataId { get; set; }
        public LotStepData? LotStepData { get; set; }
    }
}