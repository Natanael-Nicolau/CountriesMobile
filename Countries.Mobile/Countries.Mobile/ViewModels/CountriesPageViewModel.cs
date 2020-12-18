using Countries.Common.Models;
using Countries.Common.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using Countries.Mobile.EntityModels;
using Xamarin.Essentials;
using System.Threading.Tasks;
using Android.Net;

namespace Countries.Mobile.ViewModels
{
    public class CountriesPageViewModel : ViewModelBase
    {
        //Atributes        
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private readonly ILocalService _loadedData;

        private bool _isLoading;
        private bool _isLoaded;

        private string _search;
        private DelegateCommand _searchCommand;

        private ObservableCollection<CountryViewModel> _countries;


        //Constructor
        public CountriesPageViewModel(
            INavigationService navigationService,
            IApiService apiService,
            ILocalService loadedData) : base(navigationService)
        {
            Title = "Countries";
            //_countries = new ObservableCollection<CountryViewModel>();
            _navigationService = navigationService;
            _apiService = apiService;
            _loadedData = loadedData;

            IsLoading = true;
            IsLoaded = false;
            LoadCountriesAsync();
        }

        //Properties
        public string Search
        {
            get => _search;
            set
            {
                SetProperty(ref _search, value);
                ShowCountries();
            }
        }
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }
        public bool IsLoaded
        {
            get => _isLoaded;
            set => SetProperty(ref _isLoaded, value);
        }

        public DelegateCommand SearchCommand => _searchCommand ?? (_searchCommand = new DelegateCommand(ShowCountries));

        public ObservableCollection<CountryViewModel> Countries
        {
            get => _countries;
            set => SetProperty(ref _countries, value);
        }

        //Methods

        /// <summary>
        /// Downloads countries from rest API and stores them in local RAM
        /// </summary>
        private async void LoadCountriesAsync()
        {
            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.GetListAsync<CountryResponse>(url, "/rest/v2", "/all");
            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }

            _loadedData.MyCountries = (List<CountryResponse>)response.Result;
            IsLoading = false;
            IsLoaded = true;
            ShowCountries();
        }

        /// <summary>
        /// Displays the countries info to the user base on the search filter
        /// </summary>
        private void ShowCountries()
        {
            if (string.IsNullOrEmpty(Search))
            {
                Countries = new ObservableCollection<CountryViewModel>(
                    _loadedData.MyCountries
                    .Select(c => ConvertToCountryViewModel(c))
                    .ToList());
            }
            else
            {
                Countries = new ObservableCollection<CountryViewModel>(
                    _loadedData.MyCountries
                    .Select(c => ConvertToCountryViewModel(c))
                    .Where(p => p.Name.ToLower().Contains(Search.ToLower()))
                    .ToList());
            }
        }


        /// <summary>
        /// method that converts the locally stored country data to an object that is prepared to be displayed
        /// and navigated to by the user
        /// </summary>
        /// <param name="country">object that you wish to display</param>
        /// <returns></returns>
        private CountryViewModel ConvertToCountryViewModel(CountryResponse country)
        {
            return new CountryViewModel(_navigationService)
            {
                Name = country.Name,
                TopLevelDomain = country.TopLevelDomain,
                Alpha2Code = country.Alpha2Code,
                Alpha3Code = country.Alpha3Code,
                CallingCodes = country.CallingCodes,
                Capital = country.Capital,
                AltSpellings = country.AltSpellings,
                Region = country.Region,
                Subregion = country.Subregion,
                Population = country.Population,
                Latlng = country.Latlng,
                Demonym = country.Demonym,
                Area = country.Area,
                Gini = country.Gini,
                Timezones = country.Timezones,
                Borders = country.Borders,
                NativeName = country.NativeName,
                NumericCode = country.NumericCode,
                Currencies = country.Currencies,
                Languages = country.Languages,
                Translations = country.Translations,
                Flag = country.Flag,
                RegionalBlocs = country.RegionalBlocs,
                Cioc = country.Cioc,
            };
        }
    }
}
