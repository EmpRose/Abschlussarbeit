using AAIC.API.CLient.Sage100.Mobile.App.Logger;
using AAIC.API.CLient.Sage100.Mobile.App.Model;
using AAIC.API.CLient.Sage100.Mobile.Lib.Exceptions;
using AAIC.API.CLient.Sage100.Mobile.Lib.OpenId;
using AAIC.API.CLient.Sage100.Mobile.Lib.Repository.Article;
using AutoMapper;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAIC.API.CLient.Sage100.Mobile.App.ViewModel
{
    [QueryProperty(nameof(ApiSettings), "ApiSettings")]

    public partial class ArticleListPageViewModel : ObservableObject
    {
        #region QueryProperties
        public ApiSettings ApiSettings { get; set; }
        #endregion

        #region MVVM
        [ObservableProperty]
        ObservableCollection<ArticleListItem> article;

        [ObservableProperty]
        bool activityIndicatorIsVisible;
        private string articleSearch;
        public string ArticleSearch
        {
            get => articleSearch;
            set
            {
                if (SetProperty(ref articleSearch, value))
                    Task.Run(() => ExecuteSearch());
            }
        }
        #endregion

        #region Fields
        private ArticleRepository articleRepository;
        #endregion

        #region Dependency Injection
        private readonly ISageIdClient sageIdClient;
        private readonly IAppLogger logger;
        private readonly IMapper mapper;
        private readonly IHttpClientFactory httpClientFactory;
        #endregion

        // Konstruktor
        public ArticleListPageViewModel(ISageIdClient sageIdClient, IAppLogger logger, IMapper mapper, IHttpClientFactory httpClientFactory)
        {
            this.sageIdClient = sageIdClient;
            this.logger = logger;
            this.mapper = mapper;
            this.httpClientFactory = httpClientFactory;
            article = new();
        }

        private string GetFilter()
        {
            if (string.IsNullOrEmpty(ArticleSearch))
                return "";

            var filter = "(Artikelnummer eq '$filter') or (Matchcode like '%$filter%')".Replace("$filter", ArticleSearch);
            logger.Info("ArtikelstammPageViewModel/ExecuteSearch-Filter => " + filter);
            return filter;
        }

        [RelayCommand]
        public async void NavigateTo()
        {
            logger.Info("ArticleListPageViewModel/NavigateTo-Entry");
            ActivityIndicatorIsVisible = true;
            article.Clear();

            if (this.articleRepository == null)
                this.articleRepository = new ArticleRepository(sageIdClient, ApiSettings.Entitlement.EntitelmentId, ApiSettings.Dataset.DatasetKey, httpClientFactory);
            try
            {
                var result = await articleRepository.GetListAsync(GetFilter());
                if (result != null && result.resources != null)
                {
                    foreach (var item in result.resources)
                    {
                        article.Add(mapper.Map<ArticleListItem>(item));
                    }
                }
            }
            catch (HttpException e)
            {
                await Toast.Make(e.DiagnosList.diagnoses.First().message).Show();
            }
            ActivityIndicatorIsVisible = false;
            logger.Info("ArticleListPageViewModel/NavigateTo-Exit");
        }

        private async Task ExecuteSearch()
        {
            logger.Info("ArticleListPageViewModel/ExecuteSearch-Entry");
            ActivityIndicatorIsVisible = true;
            article.Clear();

            try
            {
                var result = await articleRepository.GetListAsync(GetFilter());
                if (result != null && result.resources != null)
                {
                    article.Clear();

                    foreach (var item in result.resources)
                    {
                        logger.Info("Add item: " + item.Matchcode);
                        article.Add(mapper.Map<ArticleListItem>(item));
                    }
                }
            }
            catch (HttpException e)
            {
                await Toast.Make(e.DiagnosList.diagnoses.First().message).Show();
            }

            ActivityIndicatorIsVisible = false;
            logger.Info("ArticleListPageViewModel/ExecuteSearch-Exit");
        }

        [RelayCommand]
        public async Task SelectArticle(ArticleListItem item)
        {
            await Shell.Current.GoToAsync("ArticleDetailPage",
                new Dictionary<string, object>()
                {
                    { "ApiSettings", ApiSettings },
                    { "ArticleListItem", item }
            });
        }

    }
}
