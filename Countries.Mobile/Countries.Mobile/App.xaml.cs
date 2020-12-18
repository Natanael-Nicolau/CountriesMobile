using Prism;
using Prism.Ioc;
using Countries.Mobile.ViewModels;
using Countries.Mobile.Views;
using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
using Xamarin.Forms;
using Countries.Common.Services;
using Xamarin.Essentials;

namespace Countries.Mobile
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();


            //Checks if there's an Internet Connection
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await NavigationService.NavigateAsync("NavigationPage/MainPage");
            }
            else
            {
                await NavigationService.NavigateAsync("NavigationPage/CountriesPage");
            }


        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.RegisterSingleton<ILocalService, LocalService>();
            containerRegistry.Register<IApiService, ApiService>();      
            
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
            containerRegistry.RegisterForNavigation<CountriesPage, CountriesPageViewModel>();
            containerRegistry.RegisterForNavigation<CountryDetailsPage, CountryDetailsPageViewModel>();
        }
    }
}
