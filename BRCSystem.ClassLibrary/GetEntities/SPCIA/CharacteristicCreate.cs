using BRCSystem.ClassLibrary.SPCIA.Entities;

namespace BRCSystem.ClassLibrary.GetEntities.SPCIA
{
    public static class CharacteristicCreate
    {
        public static Characteristic Get()
        {
            Random rand = new();
            return new Characteristic
            {

                Name = $"Name {rand.Next(99)}",
                UnitMeasurementId = rand.Next(99),
                UnitMeasurement = UnitMeasurementCreate.Get(),
                USL = 1,
                LSL = 1
            };
        }

        public static List<Characteristic> GetList()
        {
            var modelList = new List<Characteristic>();
            Random rand = new();

            for (int i = 0; i < 10; i++)
            {
                modelList.Add(new Characteristic
                {
                    Name = $"Name {rand.Next(99)}",
                    UnitMeasurementId = rand.Next(99),
                    UnitMeasurement = UnitMeasurementCreate.Get(),
                    USL = 1,
                    LSL = 1
                });
            }

            return modelList;
        }
    }
}
