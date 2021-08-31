
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TodaysTask.ViewModels
{
    class TestAsyncViewModel : INotifyPropertyChanged
    {
       public int i = 0;
        public event PropertyChangedEventHandler PropertyChanged;
        public TestAsyncViewModel()
        {
            AllTestData = new ObservableCollection<int>();

            TestSynchronousCommand = new Command(() =>
            {
                var j = 0;
                if (j < 3)
                {
                    TestSynchronous();
                    AllTestData.Add(TestData);
                    j++;
                    i++;
                }
            });
            TestAsynchronousCommand = new Command(() =>
            {
                var j = 0;
                if (j < 3)
                {
                    Task<int> testAsyncTask = TestAsynchronous();

                    AllTestData.Add(TestData);
                    j++;
                }
            });
            TestAsynchronousAwaitCommand = new Command(async () =>
            {
                var j = 0;
                if (j < 3)
                {
                    Task<int> testAsyncTask = TestAsynchronous();
                    int testAsync = await testAsyncTask;
                    AllTestData.Add(TestData);
                    j++;
                }
            });
            TestAsynchronousNoAwaitCommand = new Command(() =>
            {
                var j = 0;
                if (j < 3)
                {
                    AllTestData.Add(TestData);
                    j++;
                }
            });
        }
        int testData;
        public ObservableCollection<int> AllTestData { get; set; }
        public int TestData
        {
            get => testData;
            set
            {
                testData = value;

                var args = new PropertyChangedEventArgs(nameof(testData));

                PropertyChanged?.Invoke(this, args);
            }
        }
        public void TestSynchronous()
        {
            i++;
            testData = i;
            Thread.Sleep(500);
            AllTestData.Add(TestData);
        }

        public async Task<int> TestAsynchronous()
        {   
           i++; 
            testData = i;
            AllTestData.Add(TestData);
            return i;
        }

        public Command TestSynchronousCommand { get; }
        public Command TestAsynchronousCommand { get; }
        public Command TestAsynchronousAwaitCommand { get; }
        public Command TestAsynchronousNoAwaitCommand { get; }
    }
}


