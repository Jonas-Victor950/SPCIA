namespace BRCSystem.ClassLibrary.SPCIA.Entities
{
    public class MachineSetupValue
    {
        public int? Id { get; set; }

        public required double Value { get; set; }

        public int MachineProfileParameterId { get; set; }
        public virtual MachineProfileParameter? MachineProfileParameter { get; set; }
    }
}