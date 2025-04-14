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
    public class ProcessSamplingController : Controller
    {
        private readonly IProcessSamplingService _processSamplingService;

        public ProcessSamplingController(IProcessSamplingService processSamplingService)
        {
            _processSamplingService = processSamplingService;
        }

        [HttpPost]
        public IActionResult Save(ProcessSampling model)
        {
            if (ModelState.IsValid)
            {
                return Ok(_processSamplingService.Save(model));
            }

            return BadRequest(ModelState);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id) => Ok(_processSamplingService.GetById(id));

        [Authorize(Role.ADMINISTRATOR)]
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            _processSamplingService.Delete(id);

            return Ok();
        }

        [HttpPost]
        public IActionResult GetByFilter(ProcessSamplingFilterRequest filter) => Ok(_processSamplingService.GetByFilter(filter));
    }
}
