using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows;

namespace WeatherAppWPF
{
    public class ApplicationViewModel: INotifyPropertyChanged
    {
        private City selectedCity;
        DataAccess dataAccess = new DataAccess();
        CityData cityData = new CityData();
        Converter converter = new Converter();

        public void FillData(DataRow row, CityData citydata)
        {
            DateTime initTime = DateTime.Parse(citydata.RainTime[0],
                              null, DateTimeStyles.AssumeUniversal);
            string day = initTime.DayOfWeek.ToString();

            string dayFromDB = row["RainTime"].ToString().Trim();
            if (row["RainTime"].ToString().Trim() == day)
            {
                for (int i = 0; i < 8; i++)
                {
                    DateTime time = DateTime.Parse(citydata.RainTime[i],
                              null, DateTimeStyles.AssumeUniversal);
                    City cityinfo = new City();
                    cityinfo.Id = citydata.Id;
                    cityinfo.CityName = citydata.CityName;
                    cityinfo.CountryName = citydata.CountryName;
                    cityinfo.Latitude = citydata.Latitude;
                    cityinfo.Longtitude = citydata.Longtitude;
                    cityinfo.Temp = citydata.Temp[i];
                    cityinfo.MaxTemp = citydata.MaxTemp[i];
                    cityinfo.MinTemp = citydata.MinTemp[i];
                    cityinfo.RainTime = time.DayOfWeek.ToString();
                    cityinfo.Main = citydata.Main[i];
                    cityinfo.RainIn3H = citydata.RainIn3H[i];
                    cityinfo.Description = citydata.Description[i];
                    cityinfo.Time = time.ToShortTimeString();
                    Cities.Add(cityinfo);
                }
            }
            else
            {
                bool isRain = false;
                for (int i = 0; i < 8; i++)
                {
                    DateTime time = DateTime.Parse(citydata.RainTime[i],
                              null, DateTimeStyles.AssumeUniversal);
                    City cityinfo = new City();
                    cityinfo.Id = citydata.Id;
                    cityinfo.CityName = citydata.CityName;
                    cityinfo.CountryName = citydata.CountryName;
                    cityinfo.Latitude = citydata.Latitude;
                    cityinfo.Longtitude = citydata.Longtitude;
                    cityinfo.Temp = citydata.Temp[i];
                    cityinfo.MaxTemp = citydata.MaxTemp[i];
                    cityinfo.MinTemp = citydata.MinTemp[i];
                    cityinfo.RainTime = time.DayOfWeek.ToString();
                    cityinfo.Main = citydata.Main[i];
                    cityinfo.RainIn3H = citydata.RainIn3H[i];
                    cityinfo.Description = citydata.Description[i];
                    cityinfo.Time = time.ToShortTimeString();
                    Cities.Add(cityinfo);
                    if (cityinfo.RainIn3H != null)
                    {
                        isRain = true;
                    }
                }
                if (isRain == true)
                {
                    MessageBox.Show("It will rain soon!");
                }
            }           
        }

        public void FillDataRain(CityData cityData)
        {
            bool isRain = false;
            for (int i = 0; i < 8; i++)
            {
                DateTime time = DateTime.Parse(cityData.RainTime[i],
                          null, DateTimeStyles.AssumeUniversal);
                City cityinfo = new City();
                cityinfo.Id = cityData.Id;
                cityinfo.CityName = cityData.CityName;
                cityinfo.CountryName = cityData.CountryName;
                cityinfo.Latitude = cityData.Latitude;
                cityinfo.Longtitude = cityData.Longtitude;
                cityinfo.Temp = cityData.Temp[i];
                cityinfo.MaxTemp = cityData.MaxTemp[i];
                cityinfo.MinTemp = cityData.MinTemp[i];
                cityinfo.RainTime = time.DayOfWeek.ToString();
                cityinfo.Main = cityData.Main[i];
                cityinfo.RainIn3H = cityData.RainIn3H[i];
                cityinfo.Time = time.ToShortTimeString();
                cityinfo.Description = cityData.Description[i];
                Cities.Add(cityinfo);
                if (cityData.RainIn3H[i] != null)
                {
                    isRain = true;
                }
            }
            if (isRain == true)
            {
                MessageBox.Show("It will rain soon!");
            }
        }

        public ObservableCollection<City> Cities{ get; set; }

        public City SelectedCity
        {
            get { return selectedCity; }
            set
            {
                selectedCity = value;
                OnPropertyChanged("SelectedCity");
            }
        }
        public ApplicationViewModel()
        {
            Cities = new ObservableCollection<City>();
          
            DataTable tab = dataAccess.GetCityData();
            DataRow row = tab.Rows[0];
            cityData = converter.Convert(row["CityName"].ToString().Trim());
            FillData(row, cityData);
        }

        public ApplicationViewModel(string city)
        {
            Cities = new ObservableCollection<City>();
            DataTable tab = dataAccess.GetCityData();
            DataRow row = tab.Rows[0];
            cityData = converter.Convert(city);
            if (String.IsNullOrEmpty(city))
            {
                MessageBox.Show("Input name of city");
            }
            else
            {
                if (cityData == null)
                {
                    MessageBox.Show("Wrong name of city");
                }
                else
                {
                    if (row["CityName"].ToString().Trim() == city)
                    {
                        FillData(row, cityData);
                    }
                    else
                    {
                        FillDataRain(cityData);
                    }
                    bool result = dataAccess.EditCityData(cityData);
                    if (!result)
                    {
                        MessageBox.Show("Error");
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
