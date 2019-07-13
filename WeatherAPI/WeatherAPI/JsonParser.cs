using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;

namespace WeatherAPI
{
    public class JsonParser
    {
        public CityList Parsing(string name)
        {
            CityList weather = new CityList();
            weather.list = new List<List>();
            string apiKey = "7d0c4218af149a91db5cb98ba3a3d22e";
            try
            {
                HttpWebRequest apiRequest = WebRequest.Create("http://api.openweathermap.org/data/2.5/forecast?q=" + name + "&appid=" + apiKey + "&units=metric") as HttpWebRequest;
                string apiResponse = "";
                using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    apiResponse = reader.ReadToEnd();
                }
                weather = JsonConvert.DeserializeObject<CityList>(apiResponse);
            }
            
            catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.NotFound)
            {
                weather = null;
            }
            catch (WebException ex)
            {
                weather = null;
            }
            return weather;
        }
    }
}
