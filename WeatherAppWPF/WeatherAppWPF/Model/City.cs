using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace WeatherAppWPF
{
    public class City: INotifyPropertyChanged
    {
        private int id;
        private string cityname;
        private string latitude;
        private string longtitude;
        private string raintime;
        private string time;
        private string rainIn3H;
        private string temp;
        private string maxtemp;
        private string minTemp;
        private string countryname;
        private string main;
        private string description;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public string CityName
        {
            get { return cityname; }
            set
            {
                cityname = value;
                OnPropertyChanged("CityName");
            }
        }

        public string Latitude
        {
            get { return latitude; }
            set
            {
                latitude = value;
                OnPropertyChanged("Latitude");
            }
        }

        public string Longtitude
        {
            get { return longtitude; }
            set
            {
                longtitude = value;
                OnPropertyChanged("Longtitude");
            }
        }

        public string RainTime
        {
            get { return raintime; }
            set
            {
                raintime = value;
                OnPropertyChanged("RainTime");
            }
        }

        public string RainIn3H
        {
            get { return rainIn3H; }
            set
            {
                rainIn3H = value;
                OnPropertyChanged("RainIn3H");
            }
        }
   
        public string Temp
        {
            get { return temp; }
            set
            {
                temp = value;
                OnPropertyChanged("Temp");
            }
        }

        public string Time
        {
            get { return time; }
            set
            {
                time = value;
                OnPropertyChanged("Time");
            }
        }

        public string MaxTemp
        {
            get { return maxtemp; }
            set
            {
                maxtemp = value;
                OnPropertyChanged("MaxTemp");
            }
        }

        public string MinTemp
        {
            get { return minTemp; }
            set
            {
                minTemp = value;
                OnPropertyChanged("MinTemp");
            }
        }

        public string CountryName
        {
            get { return countryname; }
            set
            {
                countryname = value;
                OnPropertyChanged("CountryName");
            }
        }

        public string Main
        {
            get { return main; }
            set
            {
                main = value;
                OnPropertyChanged("Main");
            }
        } 

        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("Description");
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

