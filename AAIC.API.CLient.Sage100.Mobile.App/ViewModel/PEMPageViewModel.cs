using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AAIC.API.CLient.Sage100.Mobile.App.Model;
using AAIC.API.CLient.Sage100.Mobile.Lib.OpenId;
using AAIC.API.CLient.Sage100.Mobile.App.Logger;
using AAIC.API.CLient.Sage100.Mobile.Lib.Repository.Article;

namespace AAIC.API.CLient.Sage100.Mobile.App.ViewModel
{
    public partial class PEMPageViewModel : ObservableObject
    {
        #region QueryProperties
        public ApiSettings ApiSettings { get; set; }
        #endregion

        #region MVVM
        //[ObservableProperty]
        // ObesrvableCollection<> Post;
        // Code
        /*[ObservableProperty]
        bool activityIndicatorIsVisible;
        private string postSearch;

        public PostSearch
            {
        get => postSearch;
        set {
            if (SetProperty(ReferenceEqualityComparer postSearch, value))
                Task.Run(() => ExecuteSearch());
            }
        }*/
        #endregion

        #region Fields
        //Code
        #endregion

        #region Dependency Injection
        private readonly ISageIdClient sageIdClient;
        private readonly IAppLogger logger;
        private readonly IMapper mapper;
        private readonly IHttpClientFactory httpClientFactory;
        #endregion

        // Konstruktor

        public PEMPageViewModel(ISageIdClient sageIdClient, IAppLogger logger, IMapper mapper, IHttpClientFactory httpClientFactory)
        {
            this.sageIdClient = sageIdClient;
            this.logger = logger;
            this.mapper = mapper;
            this.httpClientFactory = httpClientFactory;
          //post = new();  <= erst MVVM beenden und die PEM Liste inkl Properties 
        }

        /*private string GetFilter()  An PEM anpassen, nach der Erstellung der Liste
        {
            if (string.IsNullOrEmpty(PostSearch))
                return "";

            var filter = "(Artikelnummer eq '$filter') or (Matchcode like '%$filter%')".Replace("$filter", PostSearch);
            logger.Info("ArtikelstammPageViewModel/ExecuteSearch-Filter => " + filter);
            return filter;
        }*/

        /*[RelayCommand]
        public async void NavigateTo()
        {
            logger.Info("PEMPageViewModel/NavigateTo-Entry");
            ActivityIndicatorIsVisible = true;
            post.Clear();

            if (this.postRepository == null)
                this.postRepository = new PostRepository(sageIdClient, ApiSettings.Entitlement.EntitelmentId, ApiSettings.Dataset.DatasetKey, httpClientFactory);
            try
            {
                var result = await postRepository.GetListAsync(GetFilter());
                if (result != null && result.resources != null)
                {
                    foreach (var item in result.resources)
                    {
                        article.Add(mapper.Map<PEMListItem>(item));
                    }
                }
            }
            catch (HttpException e)
            {
                await Toast.Make(e.DiagnosList.diagnoses.First().message).Show();
            }
            ActivityIndicatorIsVisible = false;
            logger.Info("PEMPageViewModel/NavigateTo-Exit");
        }

        private async Task ExecuteSearch()
        {
            logger.Info("PEMPageViewModel/ExecuteSearch-Entry");
            ActivityIndicatorIsVisible = true;
            article.Clear();

            try
            {
                var result = await postRepository.GetListAsync(GetFilter());
                if (result != null && result.resources != null)
                {
                    post.Clear();

                    foreach (var item in result.resources)
                    {
                        logger.Info("Add item: " + item.Matchcode);
                        post.Add(mapper.Map<PEMListItem>(item));
                    }
                }
            }
            catch (HttpException e)
            {
                await Toast.Make(e.DiagnosList.diagnoses.First().message).Show();
            }

            ActivityIndicatorIsVisible = false;
            logger.Info("PEMPageViewModel/ExecuteSearch-Exit");
        }

        [RelayCommand]
        public async Task SelectPEM(PEMListItem item)
        {
            await Shell.Current.GoToAsync("PEMDetailPage",
                new Dictionary<string, object>()
                {
                    { "ApiSettings", ApiSettings },
                    { "PEMListItem", item }
            });
        }*/
    }
}
