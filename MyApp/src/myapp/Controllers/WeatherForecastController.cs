using Microsoft.AspNetCore.Mvc;

namespace myapp.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    [Route("{location}")]
    public async Task<WeatherForecast> Get(string location, [FromServices] Dapr.Client.DaprClient daprClient)
    {
        // "statestore" the dapr component
        // no matter where the state is stored, go get it for me
        return await daprClient.GetStateAsync<WeatherForecast>("statestore", location);
    }
}
