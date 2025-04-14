using BRCSystem.ClassLibrary.GetEntities.Authorization;
using BRCSystem.ClassLibrary.GetEntities.EnrollmentSystem;
using BRCSystem.ClassLibrary.GetEntities.SPCIA;
using BRCSystem.ClassLibrary.POSStation.Entities;
using BRCSystem.ClassLibrary.SPCIA.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPCIA.API.Controllers;
using SPCIA.API.Services;
using SPCIA.API.Tests.Data;
using Xunit;

namespace SPCIA.API.Tests.ControllersTest
{
    public class ProcessControllerTest : TestWithSqlite
    {
        private readonly ProcessService _processService;
        private readonly ProcessController _processController;

        public ProcessControllerTest()
        {
            _processService = new ProcessService(DbContext);
            _processController = new ProcessController(_processService);
            DbContext.AddRange(ProductCreate.GetProductList());
            DbContext.SaveChanges();            
        }

        [Fact]
        public void SaveProcess_AddProcess_returnProcess()
        {
            var model = ProcessCreate.Get();

            var result = Assert.IsType<OkObjectResult>(_processController.Save(model));

            var modelResult = (Process)result.Value;

            Assert.Equal(result.StatusCode, 200);
            Assert.NotNull(modelResult.Id);
        }

        [Fact]
        public void SaveProcess_UpdateProcess_returnProcess()
        {
            var model = ProcessCreate.Get();
            DbContext.Processes.AddRange(model);
            DbContext.SaveChanges();

            model.ProcessName = "new Process";
            model.Characteristics = model.Characteristics.Take(model.Characteristics.Count - 1).ToList();
            model.Products = model.Products.Take(model.Products.Count - 1).ToList();

            var result = Assert.IsType<OkObjectResult>(_processController.Save(model));

            var modelResult = (Process)result.Value;

            Assert.Equal(result.StatusCode, 200);
            Assert.Equal(modelResult.ProcessName, model.ProcessName);
            Assert.Equal(modelResult.Characteristics, model.Characteristics);
            Assert.Equal(modelResult.Products, model.Products);
        }

        [Fact]
        public void DeleteProcess_ProcessId_Ok()
        {
            var model = ProcessCreate.Get();
            DbContext.Processes.AddRange(model);
            DbContext.SaveChanges();
            DbContext.ChangeTracker.Clear();

            Assert.IsType<OkResult>(_processController.Delete(model.Id!.Value));

        }

        [Fact]
        public void GetProcess_withId_returnProcess()
        {
            var model = ProcessCreate.Get();
            _processService.Save(model);

            var result = Assert.IsType<OkObjectResult>(_processController.GetById(model.Id!.Value));

            var resultModel = (Process)result.Value;

            Assert.NotNull(resultModel);
            Assert.Equal(model, resultModel);
        }

        [Fact]
        public void GetProcess_AllProcesses_returnAllProcess()
        {
            DbContext.Processes.AddRange(ProcessCreate.GetList());
            DbContext.SaveChanges();
            DbContext.ChangeTracker.Clear();

            var result = Assert.IsType<OkObjectResult>(_processController.GetAll());
            var resultModel = (List<Process>)result.Value!;
            Assert.NotNull(resultModel);
            Assert.True(resultModel.Any());
            Assert.Equal(10, resultModel.Count);
        }

        [Fact]
        public void GetProcess_AllProcessesWithStationId_returnAllProcess()
        {
            DbContext.Processes.AddRange(ProcessCreate.GetList());
            DbContext.SaveChanges();
            DbContext.ChangeTracker.Clear();

            var result = Assert.IsType<OkObjectResult>(_processController.GetbyStationId(101));
            var resultModel = (List<Process>)result.Value!;
            Assert.NotNull(resultModel);
            Assert.True(resultModel.Any());
        }

        [Fact]
        public void GetLots_LotsByProcessAndMachineId_returnLots()
        {
            DbContext.AddRange(UserCreate.GetUser());
            DbContext.AddRange(MachineCreate.GetMachineList());
            DbContext.AddRange(ProductCreate.GetProduct());
            DbContext.SaveChanges();
            DbContext.AddRange(ProcessCreate.Get());
            DbContext.SaveChanges();

            LotStepData stepData = new LotStepData();
            stepData.ProductStepId = 1;
            stepData.MachineId = 1;

            DbContext.AddRange(stepData);
            DbContext.SaveChanges();


            Random rand = new();
            ProductLot newlot = new ProductLot()
            {
                BomPn = rand.Next().ToString(),
                lotNumber = rand.Next().ToString(),
                LotQuantity = 3,
                LotType = LotType.ENG,
                MarkingInfoDateCode = rand.Next().ToString(),
                MarkingInfoLotCodeS = rand.Next().ToString(),
                OPNumber = rand.Next().ToString(),
                ProductId = 12,
                StripCode = rand.Next().ToString(),
                stripQuantity = 6,
                LotStatus = LotStatus.OnGoing,
                IntercompCode = "N/A",
                ProductLotOriginId = 1,
                LotStepDataInitId = 1,
                LotStepDatas = new List<LotStepData> { stepData },
                CreatedById = 1,
                LotPO = "M"
            };

            DbContext.AddRange(newlot);
            DbContext.SaveChanges();

            var result = Assert.IsType<OkObjectResult>(_processController.GetLotsById(1,1));
            var resultModel = (List<ProductLot>)result.Value!;
            Assert.NotNull(resultModel);
            Assert.True(resultModel.Any());
        }
    }
}
