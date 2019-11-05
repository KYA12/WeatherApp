using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherAPI
{
    public class Main
    {
        public double Temp { get; set; }
        public double Temp_min { get; set; }
        public double Temp_max { get; set; }
    }

    public class Weather
    {
        public int Id { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
    }

    public class Wind
    {
        public double Speed { get; set; }
        public double Deg { get; set; }
    }

    public class List
    {
        public Main Main { get; set; }
        public List<Weather> Weather { get; set; }
        public Wind Wind { get; set; }
        public string Dt_txt { get; set; }
        [JsonProperty("rain", NullValueHandling = NullValueHandling.Ignore)]
        public Rain Rain { get; set; }
    }

    public class Coord
    {
        public double Latitude { get; set; }
        public double Longntitude { get; set; }
    }

    public class City
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        public string Name { get; set; }
        public Coord Coord { get; set; }
        public string Country { get; set; }
    }

    public class CityList
    {
        public List<List> List { get; set; }
        public City City { get; set; }
    }

    public class Rain
    {
        [JsonProperty("3h", NullValueHandling = NullValueHandling.Ignore)]
        public double? Rain3h { get; set; }
    }
}
