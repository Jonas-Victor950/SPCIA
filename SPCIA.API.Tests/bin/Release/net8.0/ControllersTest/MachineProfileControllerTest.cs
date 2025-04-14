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
    public class MachineProfileControllerTest : TestWithSqlite
    {
        private readonly MachineProfileService _machineProfileService;
        private readonly MachineProfileController _machineProfileController;

        public MachineProfileControllerTest()
        {
            _machineProfileService = new MachineProfileService(DbContext);
            _machineProfileController = new MachineProfileController(_machineProfileService);
            DbContext.AddRange(MachineCreate.GetMachine());
            DbContext.SaveChanges();
        }

        [Fact]
        public void SaveMachineProfile_AddMachineProfile_returnMachineProfile()
        {
            var model = MachineProfileCreate.Get();

            var result = Assert.IsType<OkObjectResult>(_machineProfileController.Save(model));

            var modelResult = (MachineProfile)result.Value;

            Assert.Equal(result.StatusCode, 200);
            Assert.NotNull(modelResult.Id);
        }

        [Fact] 
        public void SaveMachineProfile_UpdateMachineProfile_returnMachineProfile() 
        {
            var model = MachineProfileCreate.Get();
            DbContext.MachineProfiles.AddRange(model);
            DbContext.SaveChanges();
            DbContext.ChangeTracker.Clear();

            model.Name = "new Name Machine";
            model.MachineProfileParameters = model.MachineProfileParameters.Take(model.MachineProfileParameters.Count - 1).ToList();
            model.Machines = model.Machines.Take(model.Machines.Count - 1).ToList();

            var result = Assert.IsType<OkObjectResult>(_machineProfileController.Save(model));

            var modelResult = (MachineProfile)result.Value;

            Assert.Equal(result.StatusCode, 200);
            Assert.Equal(modelResult.Name, model.Name);
            Assert.Equal(modelResult.MachineProfileParameters, model.MachineProfileParameters);
            Assert.Equal(modelResult.Machines, model.Machines);
        }

        [Fact]
        public void DeleteMachineProfile_MachineProfileId_Ok(){
            var model = MachineProfileCreate.Get();
            DbContext.MachineProfiles.AddRange(model);
            DbContext.SaveChanges();
            DbContext.ChangeTracker.Clear();

            Assert.IsType<OkResult>(_machineProfileController.Delete(model.Id!.Value));

        }

        [Fact] 
        public void GetMachineProfile_withId_returnMachineProfile() {
            var model = MachineProfileCreate.Get();
            _machineProfileService.Save(model);

            var result = Assert.IsType<OkObjectResult>(_machineProfileController.GetbyId(model.Id!.Value));

            var resultModel = (MachineProfile)result.Value;

            Assert.NotNull(resultModel);
            Assert.Equal(model, resultModel);           
        }

        [Fact] 
        public void GetMachineProfile_AllProfiles_returnAllMachineProfile() {
            DbContext.MachineProfiles.AddRange(MachineProfileCreate.GetList());
            DbContext.SaveChanges();
            DbContext.ChangeTracker.Clear();

            var result = Assert.IsType<OkObjectResult>(_machineProfileController.GetAll());
            var resultModel = (List<MachineProfile>)result.Value!;
            Assert.NotNull(resultModel);
            Assert.True(resultModel.Any());
            Assert.Equal(5, resultModel.Count);            
        }

        [Fact]
        public void GetMachineProfile_WithMachineId_returnAllMachineProfile()
        {
            DbContext.MachineProfiles.AddRange(MachineProfileCreate.GetList());
            DbContext.SaveChanges();

            var result = Assert.IsType<OkObjectResult>(_machineProfileController.GetAllByMachineId(2));
            var resultModel = (List<MachineProfile>)result.Value!;
            Assert.NotNull(resultModel);
            Assert.True(resultModel.Any());
        }
    }
}
