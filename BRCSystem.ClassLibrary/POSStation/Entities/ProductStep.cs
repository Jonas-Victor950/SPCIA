using BRCSystem.ClassLibrary.EnrollmentSystem.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace BRCSystem.ClassLibrary.POSStation.Entities
{
    public class ProductStep
    {
        public int? Id { get; set; }
        public int? ProcessStepId { get; set; }
        public virtual ProcessStep? ProcessStep { get; set; }
        public int? ProgramEntityId { get; set; }
        public virtual ProgramEntity? ProgramEntity { get; set; }
        public virtual List<Material>? Materials { get; set; }
        public bool SupplierPN { get; set; }
        public bool MaterialInspection { get; set; }
        public bool MaterialExposureControl { get; set; } = false;
        public bool ConditionalStation { get; set; } = false;
        public int? StartYieldId { get; set; }
        public virtual ProductStep? StartYield { get; set; }
        [NotMapped]
        public List<int> MaterialIds { get; set; }

        [NotMapped]
        public int? StartYieldStep { get; set; }
    }
}