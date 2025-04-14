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
    public class ProcessSamplingControllerTest : TestWithSqlite
    {
        private readonly ProcessSamplingService _processSamplingService;
        private readonly ProcessSamplingController _processSamplingController;

        public ProcessSamplingControllerTest()
        {
            _processSamplingService = new ProcessSamplingService(DbContext);
            _processSamplingController = new ProcessSamplingController(_processSamplingService);

            DbContext.AddRange(UserCreate.GetUser());
            DbContext.AddRange(MachineCreate.GetMachine());
            DbContext.SaveChanges();
            DbContext.AddRange(ProcessCreate.Get());
            DbContext.SaveChanges();
            DbContext.AddRange(ProcessCreate.Get());
            DbContext.SaveChanges();
        }

        [Fact]
        public void SaveProcessSampling_AddProcessSampling_ReturnProcessSampling()
        {

            var result = Assert.IsType<OkObjectResult>(_processSamplingController.Save(ProcessSamplingCreate.Get()));

            var modelResult = (ProcessSampling)result.Value!;

            Assert.True(result.StatusCode == 200);
            Assert.NotNull(modelResult);
            Assert.NotNull(modelResult.Id);

            modelResult.SampleDate = DateTime.UtcNow;

            result = Assert.IsType<OkObjectResult>(_processSamplingController.Save(modelResult));

            modelResult = (ProcessSampling)result.Value!;

            Assert.True(result.StatusCode == 200);
            Assert.NotNull(modelResult);
            Assert.Equal(modelResult.Id, 1);
        }

        [Fact]
        public void getProcessSampling_GetById_ReturnProcessSampling()
        { 
            _processSamplingService.Save(ProcessSamplingCreate.Get());

            var result = Assert.IsType<OkObjectResult>(_processSamplingController.GetById(1));

            var modelResult = (ProcessSampling)result.Value!;

            Assert.True(result.StatusCode == 200);
            Assert.NotNull(modelResult);
            Assert.Equal(modelResult.Id, 1);
        }

        [Fact]
        public void DeleteProcessSampling_RemoveProcessSampling_ReturnOk()
        {
            _processSamplingService.Save(ProcessSamplingCreate.Get());

            Assert.True(DbContext.ProcessSamplings.Any());

            var result = Assert.IsType<OkResult>(_processSamplingController.Delete(1));

            Assert.True(result.StatusCode == 200);
            Assert.True(!DbContext.ProcessSamplings.Any());
        }

        [Fact]
        public void getProcessSampling_GetByFilter_ReturnProcessSampling()
        {
            _processSamplingService.Save(ProcessSamplingCreate.Get());
            ProcessSampling ps2 = ProcessSamplingCreate.Get();
            ps2.ProcessId = 2;
            _processSamplingService.Save(ps2);

            var filter = new ProcessSamplingFilterRequest
            {
                MachineId = 1,
                ProcessId = 1
            };

            var result = Assert.IsType<OkObjectResult>(_processSamplingController.GetByFilter(filter));

            var modelResult = (List<ProcessSampling>) result.Value!;

            Assert.Equal(1, modelResult.Count);
            Assert.Null(modelResult.First().Operator.PasswordHash);
        }

    }
}
