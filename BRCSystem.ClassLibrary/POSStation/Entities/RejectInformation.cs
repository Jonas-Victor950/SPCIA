namespace BRCSystem.ClassLibrary.POSStation.Entities
{
    public class RejectInformation
    {
        public int? Id { get; set; }

        public Rejection Rejection { get; set; } = new();

        public int Count { get; set; }

        public LotStepData? LotStepData { get; set; }
    }
}