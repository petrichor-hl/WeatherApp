using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Model;
using WeatherApp.ViewModel.Commands;
using WeatherApp.ViewModel.Helpers;

namespace WeatherApp.ViewModel
{
    public class WeatherVM : INotifyPropertyChanged
    {
        // #property
        private string inputQuery;
        public string InputQuery
        {
            get => inputQuery; 
            set
            {
                inputQuery = value;
                OnPropertyChanged("InputQuery");
            }
        }

        // #property
        public ObservableCollection<City> Cities { get; set; }

        // #property
        private City selectedCity;
        public City SelectedCity
        {
            get => selectedCity;
            set
            {
                selectedCity = value;
                OnPropertyChanged("SelectedCity");
                GetCurrentConditions();
            }
        }

        // #property
        private CurrentConditions currentConditions;
        public CurrentConditions CurrentConditions
        {
            get => currentConditions; 
            set
            {
                currentConditions = value;
                OnPropertyChanged("CurrentConditions");
            }
        }

        public SearchCommand SearchCmd { get; set; }

        public WeatherVM()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                selectedCity = new City()
                {
                    LocalizedName = "New York",
                };
                currentConditions = new CurrentConditions()
                {
                    WeatherText = "Partly cloudy",
                    Temperature = new Temperature()
                    {
                        Metric = new Units()
                        {
                            Value = "21"
                        }
                    },
                };
            }

            SearchCmd = new SearchCommand(this);
            Cities = new ObservableCollection<City>();
        }

        public async void MakeQuery()
        {
            var ListOfCity = await AccuWeatherHelper.GetCities(InputQuery);

            Cities.Clear();
            foreach (var city in ListOfCity)
            {
                Cities.Add(city);
            }
        }

        private async void GetCurrentConditions()
        {
            //inputQuery = string.Empty;
            //Cities.Clear();
            if (selectedCity != null)
            {
                CurrentConditions = await AccuWeatherHelper.GetCurrentConditions(selectedCity.Key);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}