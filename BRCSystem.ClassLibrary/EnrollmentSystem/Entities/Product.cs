using BRCSystem.ClassLibrary.Authentication.Entities;
using BRCSystem.ClassLibrary.POSStation.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BRCSystem.ClassLibrary.EnrollmentSystem.Entities
{
    public class Product
    {
        public int? Id { get; set; }
        public bool Active { get; set; }
        [Required(ErrorMessage = "ProductTypeId cannot be null!")]

        public int ProductTypeId { get; set; }
        public virtual ProductType? ProductType { get; set; }

        public string Description { get; set; }
        [Required(ErrorMessage = "ProductCode cannot be null or Empty!", AllowEmptyStrings = false)]
        public string ProductCode { get; set; }

        public int? DrawingId { get; set; }
        public virtual FileModel? Drawing { get; set; }

        public int? ImageTopId { get; set; }
        public virtual FileModel? ImageTop { get; set; }
        public virtual List<ImageLabel> ImageTopLabels { get; set; }

        public int? ImageBottomId { get; set; }
        public virtual FileModel? ImageBottom { get; set; }
        public virtual List<ImageLabel> ImageBottomLabels { get; set; }

        public virtual List<ProductStep>? ProductSteps { get; set; }
        [Required(ErrorMessage = "ProcessFlowId cannot be null!")]
        public int ProcessFlowId { get; set; }
        public virtual ProcessFlow? ProcessFlow { get; set; }

        public virtual List<Capacity>? Capacities { get; set; }

        [MaxLength(2, ErrorMessage = "{0} can have a max of {1} characters")]
        public string? Prefix { get; set; }
        public int? MaxExposureThreshold { get; set; }

        public string? CloneReason { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
        public DateTime LastUpdate { get; set; } = new DateTime();

        [NotMapped]
        public bool? AbleUpdate { get; set; } = true;

        [NotMapped]
        public bool Clone { get; set; } = false;
    }
}