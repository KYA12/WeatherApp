using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WeatherAppWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ApplicationViewModel();
        }

        public void GetCityData(object sender, RoutedEventArgs e)
        {
            DataContext = new ApplicationViewModel(CityName.Text);
        }
    }
}
