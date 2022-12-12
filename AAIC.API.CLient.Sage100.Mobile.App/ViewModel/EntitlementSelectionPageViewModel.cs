using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AAIC.API.CLient.Sage100.Mobile.App.Logger;
using AAIC.API.CLient.Sage100.Mobile.App.Model;
using AAIC.API.CLient.Sage100.Mobile.Lib.Exceptions;
using AAIC.API.CLient.Sage100.Mobile.Lib.OpenId;
using AutoMapper;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AAIC.API.CLient.Sage100.Mobile.App.ViewModel
{
    
    [QueryProperty(nameof(ApiSettings), "ApiSettings" )]/* <= oder einen festen String wie : "ApiSettings"*/
    public partial class EntitlementSelectionPageViewModel : ObservableObject
    {
        
        #region QueryProperties
        public ApiSettings ApiSettings { get; set; }
        #endregion

        #region MVVM
        [ObservableProperty]
        ObservableCollection<EntitlementSelectionItem> entitlements; 

        [ObservableProperty]
        bool activityIndicatorIsVisible;
        #endregion


        #region Dependency Injection
        private readonly ISageIdClient sageIdClient;
        private readonly IAppLogger logger;
        private readonly IMapper mapper;
        #endregion
        //Konstruktor
        public EntitlementSelectionPageViewModel(ISageIdClient sageIdClient, IAppLogger logger, IMapper mapper) //EntitlementSelectionPageViewModel viewModel
        {
            
            this.sageIdClient = sageIdClient;
            this.logger = logger;
            this.mapper = mapper;
            entitlements = new();
        }

        #region Code
        [RelayCommand]
        public async void NavigateTo()
        {
            logger.Info("EntitlementSelectionPageViewModel/NavigatedTo-Entry");
            ActivityIndicatorIsVisible = true;
            entitlements.Clear();
            try
            {
                var result = await sageIdClient.GetEntitlementAsync(ApiSettings.Token);
                if(result != null)
                {
                    foreach(var item in result)
                    {
                        entitlements.Add(mapper.Map<EntitlementSelectionItem>(item));
                    }
                }
            }
            catch(HttpException e)
            {
                await Toast.Make(e.DiagnosList.diagnoses.First().message).Show();
            }

            ActivityIndicatorIsVisible = false;
            logger.Info("EntitlementSelectionPageViewModel/NavigatedTo-Exit");
        }

        [RelayCommand]
        public async Task SelectEntitlement(EntitlementSelectionItem item)
        {
            ApiSettings.Entitlement = item;
            await Shell.Current.GoToAsync("DataSetSelectionPage", new Dictionary<string, object>() { { "ApiSettings", ApiSettings } });
        }
        #endregion

    }
}
