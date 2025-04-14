using BRCSystem.ClassLibrary.EnrollmentSystem.Entities;

namespace BRCSystem.ClassLibrary.POSStation.Entities
{
    public class LotStepDataMaterial
    {
        public int? Id { get; set; }
        public int MaterialId { get; set; }
        public Material? Material { get; set; }
        public int LotStepDataId { get; set; }
        public LotStepData? LotStepData { get; set; }
        public string? BatchNumber { get; set; }
    }
}