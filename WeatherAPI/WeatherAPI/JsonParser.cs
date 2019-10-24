using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace WeatherAPI
{
    public class JsonParser
    {
        public async Task<CityList> Parsing(string name)
        {
            CityList weather = new CityList();
            weather.list = new List<List>();
            string apiKey = "";//Your API key
            try
            {
                HttpWebRequest apiRequest = WebRequest.Create("http://api.openweathermap.org/data/2.5/forecast?q=" + name + "&appid=" + apiKey + "&units=metric") as HttpWebRequest;
                string apiResponse = "";
                using (HttpWebResponse response = await apiRequest.GetResponseAsync() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    apiResponse = await reader.ReadToEndAsync();
                }
                weather = JsonConvert.DeserializeObject<CityList>(apiResponse);
            }
            
            catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.NotFound)
            {
                weather = null;
            }
            return weather;
        }
    }
}
