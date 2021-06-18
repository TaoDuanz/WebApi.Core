using Castle.Core.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Core.IService;
using WebApi.Core.Service;

namespace WebApi.Core.Controllers
{

    public class TestController : BaseController
    {
        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }




        /// <summary>
        /// 求和
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        [HttpGet]
        public int Sum(int i, int j)
        {
            ITestService testService = new TestService();
            return testService.Sum(i,j);
        }

        [HttpGet]
        public IActionResult LogTest()
        {
            _logger.LogInformation("ip:{IP},username{UserName},userid:{UserId}", "127.0.0.1", "admin", "1");
            _logger.LogDebug("debug 日志");
            _logger.LogError(new System.IO.IOException(),"io 错误");

            return Ok();
        }
    }
}
