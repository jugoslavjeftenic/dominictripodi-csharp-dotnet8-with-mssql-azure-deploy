using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using T119_AzureAppService.Data;

namespace T119_AzureAppService.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class TestController(IConfiguration config) : ControllerBase
	{
		private readonly DataContextDapper _dapper = new(config);

		[HttpGet]
		public string TestApplication()
		{
			return "Congratulation, your Application is up & running.";
		}

		[HttpGet("Connection")]
		public DateTime TestConnection()
		{
			return _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
		}
	}
}
