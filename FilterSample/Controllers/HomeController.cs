using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace FilterSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly PositionOptions _positionOptions;
        public HomeController(IOptions<PositionOptions> options)
        {
            _positionOptions = options.Value;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_positionOptions);
        }
    }
}
