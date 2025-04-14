using BRCSystem.ClassLibrary.EnrollmentSystem.Entities;

namespace BRCSystem.ClassLibrary.GetEntities.EnrollmentSystem
{
    public static class ProgramEntityCreate
    {
        public static ProgramEntity GetProgramEntity()
        {
            Random rand = new();
            return new ProgramEntity
            {
                Active = true,
                Description = $"Program Desc {rand.Next(99)}",
                Name = $"Name {rand.Next(99)}",
                Station = StationCreate.GetStation(),
            };
        }
    }
}