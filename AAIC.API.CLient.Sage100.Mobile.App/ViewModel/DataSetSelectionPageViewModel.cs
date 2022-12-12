
using AAIC.API.CLient.Sage100.Mobile.App.Logger;
using AAIC.API.CLient.Sage100.Mobile.App.Model;
using AAIC.API.CLient.Sage100.Mobile.Lib.Exceptions;
using AAIC.API.CLient.Sage100.Mobile.Lib.OpenId;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Alerts;


namespace AAIC.API.CLient.Sage100.Mobile.App.ViewModel
{
    [QueryProperty(nameof(ApiSettings), "ApiSettings")]

    public partial class DataSetSelectionPageViewModel : ObservableObject
    {
        
        #region QueryProperties
        public ApiSettings ApiSettings { get; set; }
        #endregion


        #region MVVM
        [ObservableProperty]
        ObservableCollection<DatasetSelectionItem> datasets;

        [ObservableProperty]
        bool activityIndicatorIsVisible;
        #endregion

        #region Dependency Injection
        private readonly ISageIdClient sageIdClient;
        private readonly IAppLogger logger;
        private readonly IMapper mapper;
        #endregion

        //Konstruktor
        public DataSetSelectionPageViewModel (ISageIdClient sageIdClient, IAppLogger logger, IMapper mapper)
        {
            this.sageIdClient = sageIdClient;
            this.logger = logger;
            this.mapper = mapper;
            datasets = new();
        }



        #region Code
        [RelayCommand]
        public async void NavigateTo()
        {
            logger.Info("DataSetSelectionPageViewModel/NavigatedTo-Entry");
            ActivityIndicatorIsVisible = true;
            datasets.Clear();

            try
            {
                var result = await sageIdClient.GetDatasetsAsync(ApiSettings.Token, ApiSettings.Entitlement.EntitelmentId);
                if (result != null && result.resources != null)
                {
                    foreach (var item in result.resources)
                    {
                        datasets.Add(mapper.Map<DatasetSelectionItem>(item));
                    }
                }
            }
            catch (HttpException e)
            {
                await Toast.Make(e.DiagnosList.diagnoses.First().message).Show();
            }
            ActivityIndicatorIsVisible = false;
            logger.Info("DataSetSelectionPageViewModel/NavigatedTo-Exit");
        }

        [RelayCommand]
        public async void SelectDataset(DatasetSelectionItem item)
        {
            try
            {
                ApiSettings.Dataset = item;
                await Shell.Current.GoToAsync("MainMenuPage", new Dictionary<string, object>() { { "ApiSettings", ApiSettings } });
            }
            catch (Exception e)
            {
                logger.Error(e);
            }
        }
       


        #endregion
    }
}
