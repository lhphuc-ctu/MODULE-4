using Microsoft.AspNetCore.Mvc;

namespace FilterSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigController : Controller
    {
        private readonly IConfiguration _configuration;

        public ConfigController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("bind")]
        public IActionResult ConfigBind()
        {
            var positionOptions = new PositionOptions();
            _configuration.GetSection(PositionOptions.Position).Bind(positionOptions);

            return Ok(positionOptions);
        }

        [HttpGet]
        [Route("get")]
        public IActionResult ConfigGet()
        {
            PositionOptions positionOptions = _configuration.GetSection(PositionOptions.Position).Get<PositionOptions>()!;

            return Ok(positionOptions);
        }
    }
}
