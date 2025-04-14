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
    public class MachineSetupController : Controller
    {
        private IMachineSetupService _machineSetupService;

        public MachineSetupController(IMachineSetupService machineSetupService)
        {
            this._machineSetupService = machineSetupService;
        }

        [Authorize(Role.ADMINISTRATOR, Role.ENGINEER, Role.QA)]
        [HttpPost]
        public IActionResult Save(MachineSetup model)
        {
            return Ok(_machineSetupService.Save(model));
        }

        [Authorize(Role.ADMINISTRATOR)]
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            _machineSetupService.Delete(id);
            return Ok();
        }

        [HttpGet("{id:int}")]
        public IActionResult GetbyId(int id) => Ok(_machineSetupService.GetById(id));


        [HttpGet]
        public IActionResult GetAll() => Ok(_machineSetupService.GetAll());

        [HttpGet("{machineId:int}")]
        public IActionResult GetByMachineId(int machineId) =>
            Ok(_machineSetupService.GetByMachineId(machineId));

    }
}
