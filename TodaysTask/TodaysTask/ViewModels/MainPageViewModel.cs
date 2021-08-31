using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace TodaysTask.ViewModels
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        public MainPageViewModel()
        {
            AllItems = new ObservableCollection<string>();

            DeleteCommand = new Command(() =>
            {
                TheItem = string.Empty;
            });

            SaveCommand = new Command(() =>
            {
                AllItems.Add(TheItem);

                TheItem = string.Empty;
            });

            StartWorkerCommand = new Command(() =>
            {
                AddItemWorkerThread();
            }
            );


            ToWeatherPageCommand = new Command(async () =>
            {
                var weatherPageVM = new WeatherPageViewModel();
                var weatherPage = new WeatherPage();
                weatherPage.BindingContext = weatherPageVM;
                await Application.Current.MainPage.Navigation.PushAsync(weatherPage);
            });

            ToTestAsyncPageCommand = new Command(async () =>
            {
                var TestAsyncVM = new TestAsyncViewModel();
                var TestAsync = new TestAsync();
                TestAsync.BindingContext = TestAsyncVM;
                await Application.Current.MainPage.Navigation.PushAsync(TestAsync);
            });
        }

        public ObservableCollection<string> AllItems { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        string theItem;
        public string TheItem
        {
            get => theItem;
            set
            {
                theItem = value;

                var args = new PropertyChangedEventArgs(nameof(TheItem));

                PropertyChanged?.Invoke(this, args);
            }
        }

        public void AddItemWorkerThread()
        {
            bool shouldRun = true;
            var i = 0;
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {

                theItem = "hi"+i;
                AllItems.Add(TheItem);
                i++;
                return shouldRun;
            });

        }

        public Command SaveCommand { get; }
        public Command DeleteCommand { get; }
        public Command StartWorkerCommand { get; }
        public Command ToWeatherPageCommand { get; }
        public Command ToTestAsyncPageCommand { get; }

    }
}
