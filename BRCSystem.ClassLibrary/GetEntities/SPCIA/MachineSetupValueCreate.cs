using BRCSystem.ClassLibrary.SPCIA.Entities;

namespace BRCSystem.ClassLibrary.GetEntities.SPCIA
{
    public static class MachineSetupValueCreate
    {
        public static MachineSetupValue Get()
        {
            Random rand = new();
            return new MachineSetupValue
            {
                Value = rand.Next(),
                MachineProfileParameterId = 1,
                MachineProfileParameter = MachineProfileParametersCreate.Get()
            };
        }

        public static List<MachineSetupValue> GetList()
        {
            var modelList = new List<MachineSetupValue>();
            Random rand = new();

            for (int i = 0; i < 10; i++)
            {
                modelList.Add(new MachineSetupValue
                {
                    Value = rand.Next(),
                    MachineProfileParameterId = 1,
                    MachineProfileParameter = MachineProfileParametersCreate.Get()
                });
            }

            return modelList;
        }
    }
}
