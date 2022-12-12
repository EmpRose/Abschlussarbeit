using AAIC.API.CLient.Sage100.Mobile.App.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAIC.API.CLient.Sage100.Mobile.App.ViewModel
{
    [QueryProperty(nameof(ApiSettings), "ApiSettings")]
    public partial class MainMenuPageViewModel : ObservableObject
    {

        #region QueryProperties
        private ApiSettings apiSettings;
        public ApiSettings ApiSettings
        {
            get { return apiSettings; }
            set { apiSettings = value;
                LizenzName = ApiSettings.Entitlement.DisplayName;
                DatenbankName = ApiSettings.Dataset.DisplayName; }
        }
        #endregion

        #region MVVM
        [ObservableProperty]
        string datenbankName;
        [ObservableProperty]
        string lizenzName;
        #endregion
        #region Code
        [RelayCommand]
        public async Task GoToArtikelstamm()
        {
            await Shell.Current.GoToAsync("ArticleListPage", new Dictionary<string, object>() { { "ApiSettings", ApiSettings } });
        }

        #endregion
    }
}
