using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Web.Http;
using SPAMVC.Models;

namespace SPAMVC.Controllers
{
    public class HomeController : Controller
    {
        public WeatherViewModel JsonDeserialize(string city)// Метод получения объекта json и десериализации его в модель WeatherViewModel
        {
            WeatherForecast.Forecast Forecast = new WeatherForecast.Forecast();
            WeatherCurrent.Current Current = new WeatherCurrent.Current();
            string apiKey = "7d0c4218af149a91db5cb98ba3a3d22e";// Ключ доступа к http://api.openweathermap.org
            try
            {
                /* Создаем запрос к Weather Open Api, используя название города cityName  и коюч доступа apiKey для получения прогноза погоды на 5 дней*/
                HttpWebRequest apiRequest = WebRequest.Create("http://api.openweathermap.org/data/2.5/forecast?q=" + city + "&appid=" + apiKey + "&units=metric") as HttpWebRequest;
                string apiResponseForecast = "";

                using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)// Получаем ответ от сервера и считываем его в поток StreamReader до конца
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    apiResponseForecast = reader.ReadToEnd();
                }

                Forecast = JsonConvert.DeserializeObject<WeatherForecast.Forecast>(apiResponseForecast);// Десериализуем полученый ответ в формате json в объект WeatherOpenApi.CityList
            }

            catch (WebException ex)// Если получаем любой другой тип исключения, то присваиваем объекту WeatherOpenApi.CityList значение null
            {
                Forecast = null;
            }

            try
            {
                /* Создаем запрос к Weather Open Api, используя название города cityName  и ключ доступа apiKey для получения текущей погоды*/
                HttpWebRequest apiCurrent = WebRequest.Create("http://api.openweathermap.org/data/2.5/weather?q=" + city + "&appid=" + apiKey + "&units=metric") as HttpWebRequest;// Создаем запрос к Weather Open Api, используя название города cityName  и коюч доступа apiKey
                string apiResponseCurrent = "";

                using (HttpWebResponse response = apiCurrent.GetResponse() as HttpWebResponse)// Получаем ответ от сервера и считываем его в поток StreamReader до конца
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    apiResponseCurrent = reader.ReadToEnd();
                }

                Current = JsonConvert.DeserializeObject<WeatherCurrent.Current>(apiResponseCurrent);// Десериализуем полученый ответ в формате json в объект WeatherOpenApi.CityList
            }

            catch (WebException ex)// Если получаем любой другой тип исключения, то присваиваем объекту WeatherOpenApi.CityList значение null
            {
                Current = null;
            }

            WeatherViewModel weather = new WeatherViewModel();
            weather.weatherForecast = Forecast;
            weather.weatherCurrent = Current;
            return weather;
        }

        [HttpGet]
        public ActionResult Index()//Получение ответа от сервера при запросе Get
        {
            WeatherViewModel weather = new WeatherViewModel();
            weather = JsonDeserialize("Chicago");
            return View(weather);
        }

        [HttpPost]
        public ActionResult WeatherSummary(WeatherViewModel model)
        {
            WeatherViewModel weather = new WeatherViewModel();
            weather = JsonDeserialize(model.weatherForecast.city.name);
            return View("Index", weather);
        }

    }
}

