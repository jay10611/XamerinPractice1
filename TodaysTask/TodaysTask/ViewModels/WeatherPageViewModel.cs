using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace TodaysTask.ViewModels
{
    public class WeatherPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public WeatherPageViewModel() 
        {
            SearchWeatherApiCommand = new Command(() =>
            {
                SearchWeatherApi(SearchCity);
            });
        }
        public string searchCity;
        public void SearchWeatherApi(string SearchCity)
        {

        }

        public string SearchCity
        {
            get => searchCity;
            set
            {
                searchCity = value;

                var args = new PropertyChangedEventArgs(nameof(SearchCity));

                PropertyChanged?.Invoke(this, args);
            }
        }

        public Command SearchWeatherApiCommand { get; }
    }
}
