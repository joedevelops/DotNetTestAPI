using Microsoft.AspNetCore.Mvc;

namespace testWebService.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing",
        "Bracing",
        "Chilly",
        "Cool",
        "Mild",
        "Warm",
        "Balmy",
        "Hot",
        "Sweltering",
        "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger) //constructor
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable
            .Range(1, 5)
            .Select(
                index =>
                    new WeatherForecast
                    {
                        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        TemperatureC = Random.Shared.Next(-20, 55),
                        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                    }
            )
            .ToArray();
    }

    [HttpPost]
    [Route("addforecast")]
    public ActionResult<ResponseObj> Post([FromBody] WeatherForecast newForecast)
    {
        if (ModelState.IsValid)
        {
            // Process and save the new forecast
            // For simplicity, we'll just return the forecast.
            ResponseObj responseObj = new ResponseObj {
                successMessage = "Weather forecast successfully added!",
                weatherForecast = newForecast
            };
            
            return responseObj;
        }
        else
        {
            return BadRequest(ModelState);
        }
    }
}

public class ResponseObj
{
    public string? successMessage { get; set; }
    public WeatherForecast? weatherForecast { get; set; }

}
