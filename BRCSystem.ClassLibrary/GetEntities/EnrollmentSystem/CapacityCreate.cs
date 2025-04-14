using BRCSystem.ClassLibrary.POSStation.Entities;

namespace BRCSystem.ClassLibrary.GetEntities.EnrollmentSystem
{
    public static class CapacityCreate
    {
        public static Capacity GetCapacity()
        {
            Random rand = new();

            return new Capacity
            {
                Active = true,
                CapClass = $"CapClass {rand.Next(99)}",
                //ProductCode = $"ProductCode {rand.Next(99)}",

            };
        }
    }
}