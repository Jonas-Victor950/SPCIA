using BRCSystem.ClassLibrary.SPCIA.Entities;
using BRCSystem.ClassLibrary.GetEntities.EnrollmentSystem;
namespace BRCSystem.ClassLibrary.GetEntities.SPCIA
{
    public static class ProcessCreate
    {
        public static Process Get()
        {
            Random rand = new();
            return new Process
            {
                Characteristics = CharacteristicCreate.GetList(),
                StationId = 1,
                Station = StationCreate.GetStation(),
                SampleSize = 5,
                ProcessName = $"Process {rand.Next(99)}",
                Products = ProductCreate.GetProductList()
            };
        }

        public static List<Process> GetList()
        {
            var modelList = new List<Process>();

            for (int i = 0; i < 10; i++)
            {
                modelList.Add(Get());
            }

            return modelList;
        }
    }
}
