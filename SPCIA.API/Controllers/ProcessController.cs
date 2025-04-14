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
    public class ProcessController : Controller
    {
        private IProcessService _processService;

        public ProcessController(IProcessService processService)
        {
            this._processService = processService;
        }

        [Authorize(Role.ADMINISTRATOR, Role.ENGINEER)]
        [HttpPost]
        public IActionResult Save(Process model)
        {
            return Ok(_processService.Save(model));
        }

        [Authorize(Role.ADMINISTRATOR)]
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            _processService.Delete(id);
            return Ok();
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id) => Ok(_processService.GetById(id));

        [HttpGet]
        public IActionResult GetAll() => Ok(_processService.GetAll());

        [HttpGet("{id:int}")]
        public IActionResult GetbyStationId(int id) => Ok(_processService.GetByStationId(id));

        [HttpGet("{id:int}/{machineId:int}")]
        public IActionResult GetLotsById(int id, int machineId) => Ok(_processService.GetLotsById(id, machineId));

        [HttpGet("{name}/{stationId:int}")]
        public IActionResult AlreadExistName(string name, int stationId)
        {
            return Ok(_processService.AlreadExistType(name, stationId));
        }
    }
}
