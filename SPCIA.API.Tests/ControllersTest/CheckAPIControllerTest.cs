using Microsoft.AspNetCore.Mvc;
using SPCIA.API.Controllers;
using Xunit;

namespace SPCIA.API.Tests.ControllersTest
{
    public class CheckAPIControllerTest
    {
        private readonly CheckAPIController _controller;

        public CheckAPIControllerTest()
        {
            _controller = new CheckAPIController();
        }

        [Fact]
        public void Check_Default_rout()
        {
            Assert.IsType<OkResult>(_controller.Index());
        }

        [Fact]
        public void Check_Default_Health()
        {
            Assert.IsType<OkResult>(_controller.Health());
        }
    }
}
