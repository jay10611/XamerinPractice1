using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TodaysTask.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static TodaysTask.Model.WeatherModel;

namespace TodaysTask
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeatherPage : ContentPage
    {
        public WeatherPage()
        {
            InitializeComponent();

            GetWeatherData();
        }

        private async void GetWeatherData()
        {
            var weatherList = new List<CombinedWeatherModel>();
            HttpClient client = new HttpClient();
            var response1= await client.GetStringAsync($"https://www.metaweather.com/api/location/search/?query=detroit");
            var BaseObject = JsonConvert.DeserializeObject<List<Rootobject>>(response1);
            var woeid = BaseObject.FirstOrDefault().woeid;
            var response2 = await client.GetStringAsync($"https://www.metaweather.com/api/location/{woeid}");
            var AllWeatherData = JsonConvert.DeserializeObject<Rootobject>(response2);

            var tempObj =  new CombinedWeatherModel();
            tempObj.title = BaseObject.FirstOrDefault().title;
            tempObj.currentTemp = AllWeatherData.consolidated_weather.FirstOrDefault().the_temp;
            tempObj.humidity = AllWeatherData.consolidated_weather.FirstOrDefault().humidity;
            weatherList.Add(tempObj);
            WeatherListView.ItemsSource = weatherList;
        }
    }
}