using BRCSystem.ClassLibrary.EnrollmentSystem.Entities;
using BRCSystem.ClassLibrary.POSStation.Entities;

namespace BRCSystem.ClassLibrary.GetEntities.EnrollmentSystem
{
    public static class ProductCreate
    {
        public static Product GetProduct()
        {
            Random rand = new();
            var product = new Product
            {
                Active = true,
                Description = $"Description {rand.Next(99)}",
                ProcessFlow = GetProcessFlow(),
                ProcessFlowId = 1,
                ProductCode = rand.Next().ToString(),
                ProductType = ProductTypeCreate.GetProductType(),
                ProductSteps = new List<ProductStep>(),
                Prefix = new string(Enumerable.Repeat("ABCDERFGHIJKLMNOPQRSTUVYWXZ", 2).Select(s => s[rand.Next(s.Length)]).ToArray())
            };

            for (int i = 1; i <= 5; i++)
            {
                product.ProductSteps
                .Add(new ProductStep
                {
                    ProgramEntity = ProgramEntityCreate.GetProgramEntity(),
                    MaterialIds = new List<int>() { 1 },
                    ProcessStepId = i,
                    Materials = new List<Material>() { MaterialCreate.GetMaterial() },
                    MaterialExposureControl = false
                });
            }

            return product;
        }

        public static List<Product> GetProductList()
        {
            var modelList = new List<Product>();

            for (int i = 0; i < 10; i++)
            {
                modelList.Add(GetProduct());
            }

            return modelList;
        }


        private static ProcessFlow GetProcessFlow()
        {
            Random rand = new();
            var productFlow = new ProcessFlow
            {
                Active = true,
                Name = $"Process Flow {rand.Next(99)}",
                ProductType = ProductTypeCreate.GetProductType(),
                ProcessSteps = new List<ProcessStep>()
            };

            for (int i = 1; i <= 5; i++)
            {
                productFlow.ProcessSteps.Add(new ProcessStep { Station = StationCreate.GetStation(), Step = i });
            }

            return productFlow;
        }
    }
}