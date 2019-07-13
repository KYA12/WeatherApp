using WeatherAPI;

namespace WeatherAppWPF
{
    public class Converter
    {
        public CityData Convert(string name)
        {
            JsonParser jsonParser = new JsonParser();
            CityList cityList  = new CityList();
            CityData cityData = new CityData();
            cityList = jsonParser.Parsing(name);
            if (cityList == null)
            {
                cityData = null;
            }
            else
            {
                cityData.Id = 1;
                cityData.CityName = cityList.city.name;
                cityData.CountryName = cityList.city.country;
                cityData.Latitude = cityList.city.coord.lat.ToString();
                cityData.Longtitude = cityList.city.coord.lon.ToString();
                for (int i = 0; i < 8; i++)
                {
                    cityData.RainTime.Add(cityList.list[i].dt_txt);
                    cityData.Temp.Add(cityList.list[i].main.temp.ToString());
                    cityData.MaxTemp.Add(cityList.list[i].main.temp_max.ToString());
                    cityData.MinTemp.Add(cityList.list[i].main.temp_min.ToString());
                    cityData.RainIn3H.Add(cityList.list[i].rain?.rain3h?.ToString());
                    cityData.Main.Add(cityList.list[i].weather[0].main);
                    cityData.Description.Add(cityList.list[i].weather[0].description);
                }

            }
          
            return cityData;
        }
    }
}
