using BRCSystem.ClassLibrary.GetEntities.SPCIA;
using BRCSystem.ClassLibrary.SPCIA.Entities;
using BRCSystem.ClassLibrary.Authentication.Entities;
using BRCSystem.ClassLibrary.GetEntities.SPCIA;
using Microsoft.AspNetCore.Mvc;
using SPCIA.API.Controllers;
using SPCIA.API.Services;
using SPCIA.API.Tests.Data;
using Xunit;

namespace SPCIA.API.Tests.ControllersTest
{
    public class UnitMeasurementControllerTest : TestWithSqlite
    {
        private readonly UnitMeasurementService _unitMeasurementService;
        private readonly UnitMeasurementController _unitMeasurementController;

        public UnitMeasurementControllerTest()
        {
            _unitMeasurementService = new UnitMeasurementService(DbContext);
            _unitMeasurementController = new UnitMeasurementController(_unitMeasurementService);
        }

        [Theory]
        [InlineData("Length", "m")]
        public void SaveUnitMeasurement_AddUnitMeasurement_returnUnitMeasurement(string measurement, string symbol)
        {
            var unitMeasurement = new UnitMeasurement
            {
                Measurement = measurement,
                Symbol = symbol
            };

            var result = Assert.IsType<OkObjectResult>(_unitMeasurementController.Save(unitMeasurement));
            var modelResult = (UnitMeasurement)result.Value!;

            Assert.NotNull(modelResult);
            Assert.Equal(modelResult.Measurement, unitMeasurement.Measurement);
            Assert.Equal(modelResult.Symbol, unitMeasurement.Symbol);
            Assert.NotNull(modelResult.Id);
        }

        [Theory]
        [InlineData("Length", "m")]
        public void SaveUnitMeasurement_AlterUnitMeasurement_returnUnitMeasurement(string measurement, string symbol)
        {
            var unitMeasurement = new UnitMeasurement
            {
                Measurement = measurement,
                Symbol = symbol
            };

            _unitMeasurementService.Save(unitMeasurement);

            unitMeasurement.Measurement = "Weight";
            unitMeasurement.Symbol = "kg";

            var result = Assert.IsType<OkObjectResult>(_unitMeasurementController.Save(unitMeasurement));
            var modelResult = (UnitMeasurement)result.Value!;

            Assert.NotNull(modelResult);
            Assert.Equal(modelResult.Measurement, unitMeasurement.Measurement);
            Assert.Equal(modelResult.Symbol, unitMeasurement.Symbol);
            Assert.Equal(modelResult.Id, unitMeasurement.Id);
        }

        [Theory]
        [InlineData("Length", "m")]
        public void GetUnitMeasurement_withId_returnUnitMeasurement(string measurement, string symbol)
        {
            var unitMeasurement = new UnitMeasurement
            {
                Measurement = measurement,
                Symbol = symbol
            };

            _unitMeasurementService.Save(unitMeasurement);

            var result = Assert.IsType<OkObjectResult>(_unitMeasurementController.GetbyId(unitMeasurement.Id!.Value));

            var resultModel = (UnitMeasurement)result.Value!;

            Assert.NotNull(resultModel);
            Assert.Equal(unitMeasurement, resultModel);
        }

        [Fact]
        public void GetUnitMeasurement_AllUnitMeasurement_returAllUnitMeasurement()
        {
            DbContext.UnitMeasurements.AddRange(UnitMeasurementCreate.GetList());
            DbContext.SaveChanges();

            var result = Assert.IsType<OkObjectResult>(_unitMeasurementController.GetAll());
            var resultModel = (List<UnitMeasurement>)result.Value!;
            Assert.NotNull(resultModel);
            Assert.True(resultModel.Any());
            Assert.Equal(resultModel.Count, 5);
        }

        [Theory]
        [InlineData("Length", "m")]
        public void DeleteUnitMeasurement_UnitMeasurementId_Ok(string measurement, string symbol)
        {
            var unitMeasurement = new UnitMeasurement
            {
                Measurement = measurement,
                Symbol = symbol
            };

            _unitMeasurementService.Save(unitMeasurement);

            Assert.IsType<OkResult>(_unitMeasurementController.Delete(unitMeasurement.Id!.Value));

        }
    }
}
