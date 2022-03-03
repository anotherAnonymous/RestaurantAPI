using System.Collections.Generic;

namespace RestaurantAPI
{
    public interface IWeatherForecastService
    {
        IEnumerable<WeatherForecast> Get();
        IEnumerable<WeatherForecast> Get1(int count,TemperatureRequest request);
    }
}