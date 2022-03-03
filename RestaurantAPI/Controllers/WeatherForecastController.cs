using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        
        private readonly IWeatherForecastService _service;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var result = _service.Get();
            return result;
        }

        [HttpGet("currentDay/{max}")]
        public IEnumerable<WeatherForecast> Get1([FromRoute]int max,[FromQuery] int take)
        {
            var result = _service.Get();
            return result;
        }

        [HttpPost("generate")]
        public ActionResult<IEnumerable<WeatherForecast>> Get2( int count ,TemperatureRequest request)
        {
            if (count < 1 || request.Max < request.Min)
            {
                return BadRequest();
            }
            
            else
            {
                var result = _service.Get1(count, request);
                return Ok(result);
            }
            
        }

        [HttpPost]
        public ActionResult<string> Hello([FromBody] string name)
        {
            //HttpContext.Response.StatusCode = 400;
            //return StatusCode(401, $"Hello  + { name}");

            return NotFound($"Hello {name}");
        }


    }
}
