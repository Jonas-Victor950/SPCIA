using BRCSystem.ClassLibrary.Authentication.Entities;
using BRCSystem.ClassLibrary.SPCIA.Entities;
using BRCSystem.ClassLibrary.Authorization;
using Microsoft.AspNetCore.Mvc;
using SPCIA.API.Services;

namespace SPCIA.API.Controllers
{
    [Authorize(Role.ADMINISTRATOR, Role.ENGINEER, Role.QA)]
    [ApiController]
    [Route("[controller]/[action]")]
    public class UnitMeasurementController : Controller
    {
        private readonly IUnitMeasurementService _unitMeasurementService;

        public UnitMeasurementController(IUnitMeasurementService unitMeasurementService) => _unitMeasurementService = unitMeasurementService;

        [HttpGet]
        public IActionResult GetAll() => Ok(_unitMeasurementService.GetAll());

        [HttpGet("{id:int}")]
        public IActionResult GetbyId(int id) => Ok(_unitMeasurementService.GetById(id));

        [Authorize(Role.ADMINISTRATOR)]
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            _unitMeasurementService.Delete(id);
            return Ok();
        }

        [Authorize(Role.ADMINISTRATOR, Role.ENGINEER)]
        [HttpPost]
        public IActionResult Save(UnitMeasurement model) => Ok(_unitMeasurementService.Save(model));
    }
}
