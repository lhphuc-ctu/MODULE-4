using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ConfigurationSample.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly MyInfoOptions _myInfo;

        public HomeController(IConfiguration configuration, IOptionsSnapshot<MyInfoOptions> myInfo)
        {
            _configuration = configuration;
            _myInfo = myInfo.Value;
        }

        [HttpGet]
        [Route("get")]
        public IActionResult Get()
        {
            //var myKey = _configuration.GetSection("MyKey").Get<string>()!;
            //Console.WriteLine($"My key - {_configuration["MyKey"]}");
            
            return Ok(_myInfo);
        }
    }
}
