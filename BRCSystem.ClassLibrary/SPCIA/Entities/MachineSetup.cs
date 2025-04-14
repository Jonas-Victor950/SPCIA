using BRCSystem.ClassLibrary.EnrollmentSystem.Entities;

namespace BRCSystem.ClassLibrary.SPCIA.Entities
{
    public class MachineSetup
    {
        public int? Id { get; set; }

        public int MachineId { get; set; }
        public virtual Machine? Machine { get; set; }

        public DateTime? DateTime { get; set; }

        public List<MachineSetupValue> MachineSetupValues { get; set; }

        public bool Active { get; set; }
    }
}
