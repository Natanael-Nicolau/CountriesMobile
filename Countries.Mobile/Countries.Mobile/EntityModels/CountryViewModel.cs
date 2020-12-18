using Countries.Common.Models;
using Countries.Mobile.ViewModels;
using Countries.Mobile.Views;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Countries.Mobile.EntityModels
{
    public class CountryViewModel : CountryResponse
    {
        //Atributes
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectCountryCommand;


        //Constructor
        public CountryViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        //Properties
        public DelegateCommand SelectCountryCommand => _selectCountryCommand ??
            (_selectCountryCommand = new DelegateCommand(SelectCountryAsync));

        /// <summary>
        /// Defines the navigation to the page with the country details when the model is selected
        /// </summary>
        private async void SelectCountryAsync()
        {
            NavigationParameters parameters = new NavigationParameters
            {
               { "country", this }
            };

            await _navigationService.NavigateAsync(nameof(CountryDetailsPage), parameters);
        }
    }
}
