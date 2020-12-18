using Countries.Common.Models;
using Countries.Common.Services;
using Countries.Mobile.EntityModels;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Countries.Mobile.ViewModels
{
    public class CountryDetailsPageViewModel : ViewModelBase
    {
        //Atributes
        private CountryResponse _country;

        private ObservableCollection<CountryViewModel> _borders;
        private readonly INavigationService _navigationService;
        private readonly ILocalService _loadedData;

        //Constructor
        public CountryDetailsPageViewModel(
            INavigationService navigationService,
            ILocalService loadedData) : base(navigationService)
        {
            _navigationService = navigationService;
            _loadedData = loadedData;
        }

        //Properties
        public CountryResponse Country
        {
            get => _country;
            set => SetProperty(ref _country, value);
        }

        public ObservableCollection<CountryViewModel> Borders
        {
            get => _borders;
            set => SetProperty(ref _borders, value);
        }

        /// <summary>
        /// override to define model properties when navigating to this page
        /// </summary>
        /// <param name="parameters"></param>
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("country"))
            {
                Country = parameters.GetValue<CountryResponse>("country");
                Title = Country.Name;
            }

            LoadBorders();
        }

        /// <summary>
        /// Generates the list of borders to be displayed in the page
        /// </summary>
        private void LoadBorders()
        {
            Borders = new ObservableCollection<CountryViewModel>();
            foreach (string borderA3Code in _country.Borders)
            {
                var border = _loadedData.MyCountries.Where(c => c.Alpha3Code == borderA3Code).FirstOrDefault();
                Borders.Add(ConvertToCountryViewModel(border));
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
