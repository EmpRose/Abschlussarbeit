using AAIC.API.CLient.Sage100.Mobile.App.Logger;
using AAIC.API.CLient.Sage100.Mobile.App.Model;
using AAIC.API.CLient.Sage100.Mobile.Lib.OpenId;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Maui.Alerts;

// Android Oberfläche -> Logik
namespace AAIC.API.CLient.Sage100.Mobile.App.ViewModel
{
    //vmTemplateHeader Android                  using CommunityToolkit.Mvvm.ComponentModel;
    public partial class WelcomePageViewModel : ObservableObject
    {
        //vmTemplateBody
        #region Dependency Injection
        private readonly ISageIdClient sageIdClient;
        private readonly IAppLogger logger;
        #endregion
        
        
        public WelcomePageViewModel(ISageIdClient sageIdClient, IAppLogger logger)
        {
            this.sageIdClient = sageIdClient;
            this.logger = logger;
        }
        #region Code
        [RelayCommand]
        public async Task Login()
        {
            var result = await sageIdClient.LoginAsync();
            if (result.IsError)
            {
                logger.Error(result.Error);
                //using CommunityToolkit.Maui.Alerts;
                await Toast.Make("Fehler bei der Anmeldung:" + Environment.NewLine + result.Error).Show();
            }
            else
            {
                var apiSettings = new ApiSettings
                {
                    Token = result.Token
                };
                await Shell.Current.GoToAsync("EntitlementSelectionPage",
                    new Dictionary<string, object>() { { "ApiSettings", apiSettings } });
            }
        }

        [RelayCommand]
        public async Task Logout()
        {
            await sageIdClient.LogoutAsync();
        }

        [RelayCommand]
        public async Task QuickLogin()
        {
            var result = await sageIdClient.LoginAsync();
            if (result.IsError)
            {
                logger.Error(result.Error);
                await Toast.Make("Fehler bei der Anmeldung:" + Environment.NewLine + result.Error).Show();
            }
            else
            {
                var api = new ApiSettings()
                {
                    Token = result.Token,
                    Entitlement = new EntitlementSelectionItem()
                    {
                        DisplayName = "AAIC Soft Systems (1037903)",
                        EntitelmentId = 1037903
                    },
                    Dataset = new DatasetSelectionItem()
                    {
                        DatasetKey = "OLDemoReweAbfD;123",
                        DisplayName = "Demo, Mandant: Testmann & Söhne GmbH, Frankfurt"
                    }
                };

                await Shell.Current.GoToAsync("MainMenuPage",
                    new Dictionary<string, object>() { { "ApiSettings", api } });
            }
        }
        #endregion
    }
}
