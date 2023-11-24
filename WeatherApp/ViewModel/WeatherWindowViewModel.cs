using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using WeatherApp.Model;
using WeatherApp.ViewModel.Commands;
using WeatherApp.ViewModel.Helpers;

namespace WeatherApp.ViewModel
{
    /// <summary>
    /// View model class for weather window view
    /// </summary>
    public class WeatherWindowViewModel : INotifyPropertyChanged
    {
        private string _query;
        public string Query
        {
            get { return _query; }
            set 
            { 
                _query = value; 
                OnPropertyChanged(nameof(Query));
            }
        }

        public ObservableCollection<City> Cities { get; set; }

        private CurrentCondition _currentCondition;
        public CurrentCondition CurrentCondition
        {
            get { return _currentCondition; }
            set 
            { 
                _currentCondition = value;
                OnPropertyChanged(nameof(CurrentCondition));
            }
        }

        private City _selectedCity;
        public City SelectedCity
        {
            get { return _selectedCity; }
            set 
            { 
                _selectedCity = value;
                if (SelectedCity is not null)
                {
                    OnPropertyChanged(nameof(SelectedCity));
                    GetCurrentCondition();
                }
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        public SearchCommand SearchCommand { get; set; }

        public WeatherWindowViewModel()
        {
            /*if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                SelectedCity = new City()
                {
                    LocalizedName = "London"
                };

                CurrentCondition = new CurrentCondition()
                {
                    WeatherText = "Partly Cloudy",
                    Temperature = new Temperature
                    {
                        Metric = new Units
                        {
                            Value = "21"
                        }
                    }
                };
            }*/

            // Initalize search command
            SearchCommand = new SearchCommand(this);

            // Initalize observable objects
            Cities = new ObservableCollection<City>();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void GetCurrentCondition()
        {
            // Clear query fist
            // Query = String.Empty;

            // Request updating current condition
            CurrentCondition = await AccuWeatherHelper.GetCurrentCondition(SelectedCity.Key);

            // Clear cities
            Cities.Clear();
        }

        public async void MakeQuery()
        {
            var cities = await AccuWeatherHelper.GetCities(Query);

            Cities.Clear();
            foreach (var city in cities)
            {
                Cities.Add(city);
            }
        }
    }
}
