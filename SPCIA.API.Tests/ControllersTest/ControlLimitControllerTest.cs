using BRCSystem.ClassLibrary.Authentication.Entities;
using BRCSystem.ClassLibrary.GetEntities.Authorization;
using BRCSystem.ClassLibrary.GetEntities.EnrollmentSystem;
using BRCSystem.ClassLibrary.GetEntities.SPCIA;
using BRCSystem.ClassLibrary.SPCIA.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPCIA.API.Controllers;
using SPCIA.API.Models;
using SPCIA.API.Services;
using SPCIA.API.Tests.Data;
using Xunit;

namespace SPCIA.API.Tests.ControllersTest
{
    public class ControlLimitControllerTest : TestWithSqlite
    {
        private readonly ControlLimitService _controLimitService;
        private readonly ControlLimitController _controlLimitController;

        public ControlLimitControllerTest()
        {
            _controLimitService = new ControlLimitService(DbContext);
            _controlLimitController = new ControlLimitController(_controLimitService);
            _controlLimitController.ControllerContext = new ControllerContext();
            _controlLimitController.ControllerContext.HttpContext = new DefaultHttpContext();

            DbContext.AddRange(SPCParametersCreate.GetParameters());
            DbContext.SaveChanges();
            DbContext.AddRange(UserCreate.GetUser());
            DbContext.AddRange(MachineCreate.GetMachine());
            DbContext.AddRange(CharacteristicCreate.Get());
            DbContext.SaveChanges();
            DbContext.AddRange(ProcessCreate.Get());
            DbContext.SaveChanges();
            DbContext.AddRange(ProcessCreate.Get());
            DbContext.SaveChanges();        
            DbContext.AddRange(ProcessSamplingCreate.GetWithOneCaracteristic());
            DbContext.SaveChanges();
            DbContext.AddRange(ProcessSamplingCreate.Get());
            DbContext.SaveChanges();
            DbContext.AddRange(LimitCreate.Get());
            DbContext.SaveChanges();
            _controlLimitController.ControllerContext.HttpContext.Items.Add("User", DbContext.Users.First());
        }

        [Fact]
        public void SaveControlLimit_AddControlLimit_ReturnControlLimit()
        {
            var model = ControlLimitCreate.Get();

            var result = Assert.IsType<OkObjectResult>(_controlLimitController.Save(model));

            var modelResult = (ControlLimit)result.Value;

            Assert.Equal(result.StatusCode, 200);
            Assert.NotNull(modelResult);
            Assert.Equal(modelResult.Id, 1);
        }

        [Fact]
        public void GetControlLimit_GetById_ReturnControlLimit()
        {
            var model = ControlLimitCreate.Get();
            _controlLimitController.Save(model);

            var result = Assert.IsType<OkObjectResult>(_controlLimitController.GetById(model.Id!.Value));

            var resultModel = (ControlLimit)result.Value;

            Assert.NotNull(resultModel);
            Assert.Equal(model, resultModel);
        }

        [Fact]
        public void GetControlLimit_AllControlLimits_returnAllControlLimit()
        {
            DbContext.ControlLimits.AddRange(ControlLimitCreate.GetList());
            DbContext.SaveChanges();

            var result = Assert.IsType<OkObjectResult>(_controlLimitController.GetAll());
            var resultModel = (List<ControlLimit>)result.Value!;
            Assert.NotNull(resultModel);
            Assert.True(resultModel.Any());
            Assert.Equal(10, resultModel.Count);
        }

        [Fact]
        public void GetControlLimit_AllControlLimitsByProcessId_returnAllControlLimit()
        {
            DbContext.ControlLimits.AddRange(ControlLimitCreate.GetList());
            DbContext.SaveChanges();

            var result = Assert.IsType<OkObjectResult>(_controlLimitController.GetAllByProcessId(1));
            var resultModel = (List<ControlLimit>)result.Value!;
            Assert.NotNull(resultModel);
            Assert.True(resultModel.Any());
        }

        [Fact]
        public void GetLimit_GetCalculatedLimits_returnCalculatedLimit()
        {
            var result = Assert.IsType<OkObjectResult>(_controlLimitController.GetCalculatedLimits(1, 1, 22));
            var resultModel = (Limit)result.Value!;
            Assert.NotNull(resultModel);
        }

        [Fact]
        public void GetLimit_GetCalculatedLimitsByCpk_returnCalculatedLimit()
        {
            var result = Assert.IsType<OkObjectResult>(_controlLimitController.GetCalculatedLimitsByCpk(1, 1, 22, 0.7));
            var resultModel = (Limit)result.Value!;
            Assert.NotNull(resultModel);
        }
    }
}
