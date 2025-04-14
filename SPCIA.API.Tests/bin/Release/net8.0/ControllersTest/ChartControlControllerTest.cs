using BRCSystem.ClassLibrary.GetEntities.Authorization;
using BRCSystem.ClassLibrary.GetEntities.EnrollmentSystem;
using BRCSystem.ClassLibrary.GetEntities.SPCIA;
using BRCSystem.ClassLibrary.SPCIA.Entities;
using Microsoft.AspNetCore.Mvc;
using SPCIA.API.Controllers;
using SPCIA.API.Models;
using SPCIA.API.Services;
using SPCIA.API.Tests.Data;
using Xunit;

namespace SPCIA.API.Tests.ControllersTest
{
    public class ChartControlControllerTest : TestWithSqlite
    {
        private readonly ChartControlService _chartControlService;
        private readonly ChartControlController _chartControlController;

        public ChartControlControllerTest()
        {
            _chartControlService = new ChartControlService(DbContext);
            _chartControlController = new ChartControlController(_chartControlService);

            DbContext.AddRange(SPCParametersCreate.GetParameters());
            DbContext.SaveChanges();
            DbContext.AddRange(UserCreate.GetUser());
            DbContext.AddRange(MachineCreate.GetMachine());
            DbContext.AddRange(CharacteristicCreate.Get());
            DbContext.SaveChanges();
            DbContext.AddRange(ProcessCreate.Get());
            DbContext.SaveChanges();
            DbContext.AddRange(ProcessSamplingCreate.GetWithOneCaracteristic());
            DbContext.SaveChanges();
            DbContext.AddRange(ProcessSamplingCreate.Get());
            DbContext.SaveChanges();
            DbContext.AddRange(ControlLimitCreate.Get());
            DbContext.SaveChanges();
            DbContext.AddRange(LimitCreate.Get());
            DbContext.SaveChanges();
            DbContext.AddRange(ControlLimitCreate.Get());
            DbContext.SaveChanges();
            DbContext.AddRange(LimitCreate.Get());
            DbContext.SaveChanges();
        }

        [Fact]
        public void GetChartControlData_GetByFilter_ReturnLimitCalculationResponse()
        {
            ChartControlFilterRequest request = new ChartControlFilterRequest();
            request.CharacteristicId = 12;
            request.ProcessId = 1;
            request.MachineId = 1;
            request.SampleSize = 30;

            DbContext.Limits.First().CharacteristicId = 12;
            DbContext.SaveChanges();

            var result = Assert.IsType<OkObjectResult>(_chartControlController.GetByFilter(request));
            var modelResult = (ChartControlResponse)result.Value!;

            Assert.True(result.StatusCode == 200);
            Assert.NotNull(modelResult);
        }

        [Fact]
        public void GetChartControlData_CheckForAlarm_ReturnBoolean()
        {
            DbContext.Limits.First().CharacteristicId = 12;
            DbContext.SaveChanges();
            String ids = "12";
            var result = Assert.IsType<OkObjectResult>(_chartControlController.CheckInputForAlarm(1,1,ids));
            var modelResult = (Boolean)result.Value!;

            Assert.True(result.StatusCode == 200);
            Assert.False(modelResult);
        }

        [Fact]
        public void GetChartControlData_GetLimitHistory_ReturnListLimitCalculationResponse()
        {
            ChartControlFilterRequest request = new ChartControlFilterRequest();
            request.CharacteristicId = 12;
            request.ProcessId = 1;
            request.MachineId = 1;
            request.SampleSize = 30;

            DbContext.Limits.First().CharacteristicId = 12;
            DbContext.SaveChanges();

            var result = Assert.IsType<OkObjectResult>(_chartControlController.GetLimitHistory(request));
            var modelResult = result.Value!;

            Assert.True(result.StatusCode == 200);
            Assert.NotNull(modelResult);
        }
    }
}
