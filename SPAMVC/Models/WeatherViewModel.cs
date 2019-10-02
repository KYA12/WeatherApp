using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace SPAMVC.Models
{
    /* Модель данных, которую мы получаем, при десериализации json http://api.openweathermap.org/data/2.5/forecast?q={cityname} */
    public class WeatherForecast
    {
        public class Main
            {
                public double temp { get; set; }
                public double temp_min { get; set; }
                public double temp_max { get; set; }
            }

            public class Weather
            {
                public int id { get; set; }
                public string main { get; set; }
                public string description { get; set; }
            }

            public class Wind
            {
                public double speed { get; set; }
                public double deg { get; set; }
            }

            public class List
            {
                public Main main { get; set; }
                public List<Weather> weather { get; set; }
                public Wind wind { get; set; }
                public string dt_txt { get; set; }
                [JsonProperty("rain", NullValueHandling = NullValueHandling.Ignore)]
                public Rain rain { get; set; }
            }

            public class Coord
            {
                public double lat { get; set; }
                public double lon { get; set; }
            }

            public class City
            {
                public int id { get; set; }
                [Required(ErrorMessage = "Please, enter name of city")]
                [Display(Name = "City")]
                public string name { get; set; }
                public Coord coord { get; set; }
                public string country { get; set; }
            }

            public class Forecast
            {
                public List<List> list { get; set; }
                public City city { get; set; }
            }

            public class Rain
            {
                [JsonProperty("3h", NullValueHandling = NullValueHandling.Ignore)]
                public double? rain3h { get; set; }
            }

        }

        public class WeatherCurrent
        {
            public class Coord
            {
                public double lon { get; set; }
                public double lat { get; set; }
            }

            public class Weather
            {
                public int id { get; set; }
                public string main { get; set; }
                public string description { get; set; }
            }
            public class Main
            {
                public double temp { get; set; }
                public double temp_min { get; set; }
                public double temp_max { get; set; }
            }

            public class Wind
            {
                public double speed { get; set; }
                public double deg { get; set; }
            }

            public class Sys
            {
                public string country { get; set; }
            }

            public class Current
            {
                public Coord coord { get; set; }
                public List<Weather> weather { get; set; }
                public Main main { get; set; }
                public Wind wind { get; set; }
                public int dt { get; set; }
                public Sys sys { get; set; }
                public string name { get; set; }
            }
        }

        public class WeatherViewModel
        {
            public WeatherForecast.Forecast weatherForecast { get; set; }
            public WeatherCurrent.Current weatherCurrent { get; set; }
            public WeatherViewModel()
            {
                weatherForecast = new WeatherForecast.Forecast();
                weatherCurrent = new WeatherCurrent.Current();
            }
        }
 
}
