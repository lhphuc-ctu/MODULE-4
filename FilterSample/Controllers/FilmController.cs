using FilterSample.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FilterSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[MyActionFilter("Controller")]
    public class FilmController : Controller
    {
        private static List<string> films = new() { "Breaking Bad", "Better Call Saul", "The last of Us", "Doctor Who", "House MD" };

        [HttpGet]
        //[MyAsyncFilter("Action")]
        //[MyResourceFilter("Action")]
        public IActionResult GetList()
        {
            Console.WriteLine($"{DateTime.Now:hh:mm:ss:ffff} - FilmController - Get List");
            return Ok(films);
        }

        [HttpGet("{id:int}")]
        //[ServiceFilter(typeof(MyServiceFilter))]
        public IActionResult GetFlim(int id)
        {
            Console.WriteLine($"{DateTime.Now:hh:mm:ss:ffff} - FilmController - Get Films {id}");
            return Ok(films[id - 1]);
        }

        [HttpPost]
        public IActionResult PostFilm(string filmName)
        {
            Console.WriteLine($"{DateTime.Now:hh:mm:ss:ffff} - FilmController - Post Films {filmName}");
            films.Add(filmName);
            return Ok(films);
        }

        [HttpPost("{id:int}")]
        public IActionResult PostFilm(int id, string filmName)
        {
            Console.WriteLine($"{DateTime.Now:hh:mm:ss:ffff} - FilmController - Update Films {id} name to {filmName} ");
            films[id - 1] = filmName;
            return Ok(films);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteFilm(int id)
        {
            Console.WriteLine($"{DateTime.Now:hh:mm:ss:ffff} - FilmController - Delete Film {id}");
            var name = films[id - 1];
            films.Remove(name);
            return Ok(films);
        }
    }
}
