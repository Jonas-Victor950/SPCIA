using BRCSystem.ClassLibrary.EnrollmentSystem.Entities;

namespace BRCSystem.ClassLibrary.GetEntities.EnrollmentSystem
{
    public static class ProductTypeCreate
    {
        public static ProductType GetProductType()
        {
            Random rand = new();
            return new ProductType
            {
                Active = true,
                Prefix = $"SD",
                Type = $"Type {rand.Next(99)}"
            };
        }

        public static List<ProductType> GetProductTypeList()
        {
            var productTypes = new List<ProductType>();
            for (int k = 0; k < 5; ++k)
            {
                var productType = new ProductType
                {
                    Active = true,
                    Prefix = $"SD",
                    Type = $"Type {k}"
                };
                productTypes.Add(productType);
            }
            return productTypes;
        }
    }
}