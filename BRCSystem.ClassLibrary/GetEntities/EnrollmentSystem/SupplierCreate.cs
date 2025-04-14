using BRCSystem.ClassLibrary.EnrollmentSystem.Entities;

namespace BRCSystem.ClassLibrary.GetEntities.EnrollmentSystem
{
    public static class SupplierCreate
    {
        public static Supplier GetSupplier()
        {
            Random rand = new();
            return new Supplier
            {
                Active = true,
                Name = $"Supplier {rand.Next(99)}"
            };
        }
    }
}