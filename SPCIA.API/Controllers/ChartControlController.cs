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
    public class ChartControlController : Controller
    {
        private readonly IChartControlService _chartControlService;

        public ChartControlController(IChartControlService chartControlService)
        {
            _chartControlService = chartControlService;
        }

        [HttpPost]
        public IActionResult GetByFilter(ChartControlFilterRequest filter) => Ok(_chartControlService.GetByFilter(filter));

        [HttpGet("{machineId:int}/{processId:int}")]
        public IActionResult CheckInputForAlarm(int machineId, int processId, [FromQuery] string characteristicIds)
        {
            List<int> ids = characteristicIds.Split(',').Select(int.Parse).ToList();
            return Ok(_chartControlService.CheckForAlarm(machineId, processId, ids));
        }

        [HttpPost]
        public IActionResult GetLimitHistory(ChartControlFilterRequest filter) => Ok(_chartControlService.GetLimitHistory(filter));
    }
}
