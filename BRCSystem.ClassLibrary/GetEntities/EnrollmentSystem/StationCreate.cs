using BRCSystem.ClassLibrary.EnrollmentSystem.Entities;
using BRCSystem.ClassLibrary.POSStation.Entities;

namespace BRCSystem.ClassLibrary.GetEntities.EnrollmentSystem
{
    public static class StationCreate
    {
        public static Station GetStation()
        {
            Random rand = new();

            return new Station
            {
                Active = true,
                Name = $"Station {rand.Next(99)}",
                Room = Room.FOL,
                HasSplit = false,
                Rejections = new List<Rejection>()
                                    {
                                        GetRejection(),
                                        GetRejection(),
                                        GetRejection()
                                    }
            };
        }

        private static Rejection GetRejection()
        {
            Random random = new();
            return new Rejection
            {
                Description = $"Rejection {random.Next()}",
                Active = true
            };
        }
    }
}