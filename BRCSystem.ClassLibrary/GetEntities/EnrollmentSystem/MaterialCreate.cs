using BRCSystem.ClassLibrary.EnrollmentSystem.Entities;

namespace BRCSystem.ClassLibrary.GetEntities.EnrollmentSystem
{
    public static class MaterialCreate
    {
        public static Material GetMaterial()
        {
            Random rand = new();

            return new Material
            {
                Active = true,
                Description = $"Material Desc {rand.Next(99)}",
                MaterialType = MaterialTypeCreate.GetMaterialType(),
                PartNumber = rand.Next(999).ToString(),
                Supplier = SupplierCreate.GetSupplier(),
                SupplierPn = $"SupplierPn {rand.Next(99)}",
                StripColumns = 4,
                StripRows = 4,
            };
        }
    }
}