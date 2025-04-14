using BRCSystem.ClassLibrary.Authentication.Entities;
using BRCSystem.ClassLibrary.EnrollmentSystem.Entities;
using System.ComponentModel.DataAnnotations;

namespace BRCSystem.ClassLibrary.POSStation.Entities
{
    public class ProductLot
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "LotType cannot be null!")]
        public LotType LotType { get; set; }

        [Required(ErrorMessage = "ProductId cannot be null!")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public string? BomPn { get; set; }

        [Required(ErrorMessage = "lotNumber cannot be null or empty!", AllowEmptyStrings = false)]
        [MaxLength(50, ErrorMessage = "lotNumber allows a maximum of 50 characters!")]
        public string lotNumber { get; set; }

        [MaxLength(2, ErrorMessage = "StripCode allows a maximum of 2 characters!")]
        public string? StripCode { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "LotQuantity cannot be negative!")]
        public int? stripQuantity { get; set; }

        [Required(ErrorMessage = "LotQuantity cannot be null!")]
        [Range(0, int.MaxValue, ErrorMessage = "LotQuantity cannot be negative!")]
        public int LotQuantity { get; set; }

        public string? OPNumber { get; set; }

        [MaxLength(3, ErrorMessage = "MarkingInfoLotCodeS allows a maximum of 3 characters!")]
        public string? MarkingInfoLotCodeS { get; set; }

        public string? MarkingInfoDateCode { get; set; }

        [Required(ErrorMessage = "CreatedBy cannot be null!")]
        public int CreatedById { get; set; }
        public User? CreatedBy { get; set; }

        public DateTime? DateCreated { get; set; }

        public LotStatus LotStatus { get; set; } = LotStatus.WIP;

        public DateTime? LotStart { get; set; }

        public DateTime? LotEnd { get; set; }

        public List<LotStepData> LotStepDatas { get; set; }

        public LTCConfig? LTCConfig { get; set; }

        public bool Active { get; set; } = true;

        public int? ProductLotOriginId { get; set; }
        public ProductLot? ProductLotOrigin { get; set; }

        public int? RejectionInit { get; set; }

        public int? LotStepDataInitId { get; set; }
        public LotStepData? LotStepDataInit { get; set; }
        public string? StripsSplitted { get; set; }
        public List<LotStatusHistory>? LotStatusHistories { get; set; }
        public DateTime? MaterialExposureControlInitTime { get; set; }

        [Required(ErrorMessage = "IntercompCode cannot be null or empty!", AllowEmptyStrings = false)]
        [MaxLength(20, ErrorMessage = "IntercompCode allows a maximum of 20 characters!")]
        public string IntercompCode { get; set; } = "N/A";

        [MaxLength(25, ErrorMessage = "LotPO allow a maximum of 25 characters!")]
        public string LotPO { get; set; }
    }
}