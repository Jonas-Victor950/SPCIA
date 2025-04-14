using BRCSystem.ClassLibrary.EnrollmentSystem.Entities;
using BRCSystem.ClassLibrary.POSStation.Entities;

namespace BRCSystem.ClassLibrary.GetEntities.EnrollmentSystem
{
    public static class MaterialTypeCreate
    {
        public static MaterialType GetMaterialType()
        {
            Random rand = new();
            return new MaterialType
            {
                Active = true,
                Type = $"Type {rand.Next(99)}",
                Category = Category.DIRECT,
                HasStrip = true
            };
        }
    }
}