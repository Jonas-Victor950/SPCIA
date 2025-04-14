using BRCSystem.ClassLibrary.SPCIA.Entities;
using BRCSystem.ClassLibrary.GetEntities.EnrollmentSystem;

namespace BRCSystem.ClassLibrary.GetEntities.SPCIA
{
    public static class MachineProfileCreate
    {
        public static MachineProfile Get()
        {
            Random rand = new();
            return new MachineProfile
            {
                Active = true,
                Name = $"Name {rand.Next(99)}",
                MachineProfileParameters = MachineProfileParametersCreate.GetList(),
                Machines = MachineCreate.GetMachineList()
            };
        }

        public static List<MachineProfile> GetList()
        {
            List<MachineProfile> profiles = new List<MachineProfile>();
            for (int i = 0; i < 5; i++)
            {
                profiles.Add(Get());
            }
            return profiles;
        }
    }
}
