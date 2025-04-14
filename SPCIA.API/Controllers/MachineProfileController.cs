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
    public class MachineProfileController : Controller
    {
        private IMachineProfileService _machineProfileService;

        public MachineProfileController(IMachineProfileService machineProfileService)
        {
            this._machineProfileService = machineProfileService;
        }

        [Authorize(Role.ADMINISTRATOR, Role.ENGINEER)]
        [HttpPost]
        public IActionResult Save(MachineProfile model)
        {
            return Ok(_machineProfileService.Save(model));
        }

        [Authorize(Role.ADMINISTRATOR)]
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
           _machineProfileService.Delete(id);
            return Ok();
        }

        [HttpGet("{id:int}")]
        public IActionResult GetbyId(int id) => Ok(_machineProfileService.GetById(id));      
        

        [HttpGet]
        public IActionResult GetAll() => Ok(_machineProfileService.GetAll());

        [HttpGet("{machineId:int}")]
        public IActionResult GetAllByMachineId(int machineId) => 
            Ok(_machineProfileService.GetByMachineId(machineId));
           
    }
}
