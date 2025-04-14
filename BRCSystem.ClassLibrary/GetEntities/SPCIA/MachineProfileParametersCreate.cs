using BRCSystem.ClassLibrary.SPCIA.Entities;

namespace BRCSystem.ClassLibrary.GetEntities.SPCIA
{
    public static class MachineProfileParametersCreate
    {
        public static MachineProfileParameter Get()
        {
            Random rand = new();
            return new MachineProfileParameter
            {
                Name = $"Name {rand.Next(99)}",
                UnitMeasurementId = 1,
                UnitMeasurement = UnitMeasurementCreate.Get()
            };
        }

        public static List<MachineProfileParameter> GetList()
        {
            var modelList = new List<MachineProfileParameter>();
            Random rand = new();

            for (int i = 0; i < 10; i++)
            {
                modelList.Add(new MachineProfileParameter
                {
                    Name = $"Name {rand.Next(99)}",
                    UnitMeasurementId = i + 1,
                    UnitMeasurement = UnitMeasurementCreate.Get()
                });
            }

            return modelList;
        }
    }
}
