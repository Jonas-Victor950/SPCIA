using BRCSystem.ClassLibrary.SPCIA.Entities;
using BRCSystem.ClassLibrary.GetEntities.EnrollmentSystem;

namespace BRCSystem.ClassLibrary.GetEntities.SPCIA
{
    public class MachineSetupCreate
    {
        public static MachineSetup Get()
        {
            Random rand = new();
            return new MachineSetup
            {
                Active = true,
                MachineId = 1,
                DateTime = new DateTime(),
                MachineSetupValues = MachineSetupValueCreate.GetList()
            };
        }

        public static List<MachineSetup> GetList()
        {
            List<MachineSetup> setups = new List<MachineSetup>();
            for (int i = 0; i < 5; i++)
            {
                setups.Add(Get());
            }
            return setups;
        }
    }
}
