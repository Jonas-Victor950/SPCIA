using BRCSystem.ClassLibrary.GetEntities.EnrollmentSystem;
using BRCSystem.ClassLibrary.GetEntities.SPCIA;
using BRCSystem.ClassLibrary.SPCIA.Entities;
using Microsoft.AspNetCore.Mvc;
using SPCIA.API.Controllers;
using SPCIA.API.Services;
using SPCIA.API.Tests.Data;
using Xunit;

namespace SPCIA.API.Tests.ControllersTest
{
    public class MachineSetupControllerTest : TestWithSqlite
    {
        private readonly MachineSetupService _machineSetupService;
        private readonly MachineSetupController _machineSetupController;

        public MachineSetupControllerTest()
        {
            _machineSetupService = new MachineSetupService(DbContext);
            _machineSetupController = new MachineSetupController(_machineSetupService);
            DbContext.AddRange(MachineCreate.GetMachine());
            DbContext.SaveChanges();
        }

        [Fact]
        public void SaveMachineSetup_AddMachineSetup_returnMachineSetup()
        {
            var model = MachineSetupCreate.Get();

            var result = Assert.IsType<OkObjectResult>(_machineSetupController.Save(model));

            var modelResult = (MachineSetup)result.Value;

            Assert.Equal(result.StatusCode, 200);
            Assert.NotNull(modelResult.Id);
        }

        [Fact]
        public void SaveMachineSetup_UpdateMachineSetup_returnMachineSetup()
        {
            var model = MachineSetupCreate.Get();
            DbContext.MachineSetups.AddRange(model);
            DbContext.SaveChanges();
            DbContext.ChangeTracker.Clear();

            model.Machine.Stations.Clear();

            model.DateTime = DateTime.Now.AddMinutes(30);

            var result = Assert.IsType<OkObjectResult>(_machineSetupController.Save(model));

            var modelResult = (MachineSetup)result.Value;

            Assert.Equal(result.StatusCode, 200);
            Assert.Equal(modelResult.DateTime, model.DateTime);
        }

        [Fact]
        public void DeleteMachineSetup_MachineSetupId_Ok()
        {
            var model = MachineSetupCreate.Get();
            DbContext.MachineSetups.AddRange(model);
            DbContext.SaveChanges();
            DbContext.ChangeTracker.Clear();

            Assert.IsType<OkResult>(_machineSetupController.Delete(model.Id!.Value));
        }

        [Fact]
        public void GetMachineSetup_withId_returnMachineSetup()
        {
            var model = MachineSetupCreate.Get();
            _machineSetupService.Save(model);

            var result = Assert.IsType<OkObjectResult>(_machineSetupController.GetbyId(model.Id!.Value));

            var resultModel = (MachineSetup)result.Value;

            Assert.NotNull(resultModel);
            Assert.Equal(model, resultModel);
        }

        [Fact]
        public void GetMachineSetup_AllSetups_returnAllMachineSetup()
        {
            DbContext.MachineSetups.AddRange(MachineSetupCreate.GetList());
            DbContext.SaveChanges();
            DbContext.ChangeTracker.Clear();

            var result = Assert.IsType<OkObjectResult>(_machineSetupController.GetAll());
            var resultModel = (List<MachineSetup>)result.Value!;
            Assert.NotNull(resultModel);
            Assert.True(resultModel.Any());
            Assert.Equal(5, resultModel.Count);
        }

        [Fact]
        public void GetMachineSetup_WithMachineId_returnAllMachineSetup()
        {
            DbContext.MachineSetups.AddRange(MachineSetupCreate.GetList());
            DbContext.SaveChanges();
            DbContext.ChangeTracker.Clear();

            var result = Assert.IsType<OkObjectResult>(_machineSetupController.GetByMachineId(1));
            var resultModel = (List<MachineSetup>)result.Value!;
            Assert.NotNull(resultModel);
            Assert.True(resultModel.Any());
            Assert.Equal(5, resultModel.Count);
        }
    }
}
