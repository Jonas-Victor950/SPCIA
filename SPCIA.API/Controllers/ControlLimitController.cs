using BRCSystem.ClassLibrary.Authentication.Entities;
using BRCSystem.ClassLibrary.Authorization;
using BRCSystem.ClassLibrary.SPCIA.Entities;
using Microsoft.AspNetCore.Mvc;
using SPCIA.API.Models;
using SPCIA.API.Services;

namespace SPCIA.API.Controllers
{
    [Authorize(Role.ADMINISTRATOR, Role.ENGINEER, Role.QA)]
    [ApiController]
    [Route("[controller]/[action]")]
    public class ControlLimitController : Controller
    {
        private readonly IControlLimitService _controlLimitService;

        public ControlLimitController(IControlLimitService controlLimitService)
        {
            _controlLimitService = controlLimitService;
        }

        [Authorize(Role.ADMINISTRATOR, Role.ENGINEER)]
        [HttpPost]
        public IActionResult Save(ControlLimit model)
        {
            if (ModelState.IsValid)
            {
                return Ok(_controlLimitService.Save(model));
            }

            return BadRequest(ModelState);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id) => Ok(_controlLimitService.GetById(id));


        [HttpGet]
        public IActionResult GetAll() => Ok(_controlLimitService.GetAll());

        [HttpGet("{id:int}")]
        public IActionResult GetAllByProcessId(int id)
        {
            return Ok(_controlLimitService.GetAllByProcessId(id));
        }

        [HttpGet("{machineId:int}/{processId:int}/{characteristicId:int}")]
        public IActionResult GetCalculatedLimits(int machineId, int processId, int characteristicId) {
            return Ok(_controlLimitService.getCalculatedLimits(machineId, processId, characteristicId));
        }

        [HttpGet("{machineId:int}/{processId:int}/{characteristicId:int}/{cpk:double}")]
        public IActionResult GetCalculatedLimitsByCpk(int machineId, int processId, int characteristicId, double cpk)
        {
            return Ok(_controlLimitService.getCalculatedLimitsByCpk(machineId, processId, characteristicId, cpk));
        }
    }
}
