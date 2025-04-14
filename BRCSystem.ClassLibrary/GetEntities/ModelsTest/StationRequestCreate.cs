using BRCSystem.ClassLibrary.EnrollmentSystem.Entities;

namespace BRCSystem.ClassLibrary.GetEntities.ModelsTest
{
    public static class StationRequestCreate
    {
        public static Station GetStation()
        {
            Random rand = new();

            return new Station
            {

                Id = 1,
                Active = true,
                Name = $"Station Name {rand.Next(99)}",
                Description = $"Station Desc {rand.Next(99)}"
            };
        }
    }
}