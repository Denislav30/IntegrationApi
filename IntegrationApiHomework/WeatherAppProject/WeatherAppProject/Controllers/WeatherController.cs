using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace WeatherAppProject.Controllers
{
    public class WeatherController : Controller
    {
        //public const string key = "ee835c9ca5ca4cf9a8098d8d298dbfb8";
        public const string key = "c40a681d7c5d4a56950299209225349e";
        public IActionResult Index(string city_name, string country_name)
        {
            ViewData["hello_user"] = "Enter your town's name and your country name initials e.g(BG) to view the daily weather for your specific location.";
            ViewData["weatherInfo"] = "Weather Information:";

            var url = "https://api.weatherbit.io/v2.0/current?&city=" + city_name + "&country=" + country_name + "&key=" + key + "&include=minutely"; 

            var body = "";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var response = client.GetAsync("").Result;
                body = response.Content.ReadAsStringAsync().Result;

                JObject dataInfo = JObject.Parse(body);

                if (dataInfo["data"] != null && dataInfo["data"].ToString() != "")
                {
                    ViewData["cityName"] = "City name: " + dataInfo["data"][0]["city_name"];
                    ViewData["temperature"] = "Temperature(Celsium): " + dataInfo["data"][0]["app_temp"];
                    ViewData["dateTime"] = "Date Time: " + dataInfo["data"][0]["datetime"];
                    ViewData["sunrise"] = "Sunrise: " + dataInfo["data"][0]["sunrise"];
                    ViewData["sunset"] = "Sunset: " + dataInfo["data"][0]["sunset"];
                    ViewData["timezone"] = "Time Zone: " + dataInfo["data"][0]["timezone"];
                }

            }
            ViewData["body"] = body;
            return View();
        }
    }
}
