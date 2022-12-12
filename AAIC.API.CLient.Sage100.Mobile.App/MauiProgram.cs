using CommunityToolkit.Maui;
using AutoMapper;
using AAIC.API.CLient.Sage100.Mobile.App.View;
using AAIC.API.CLient.Sage100.Mobile.App.ViewModel;
using AAIC.API.CLient.Sage100.Mobile.App.Logger;
using AAIC.API.CLient.Sage100.Mobile.Lib.OpenId;
using AAIC.API.CLient.Sage100.Mobile.Lib.Token;



namespace AAIC.API.CLient.Sage100.Mobile.App { 
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // AutoMapper
        var autoMapperConfig = new MapperConfiguration(c =>
        {
            c.CreateMap<Entitlement, Model.EntitlementSelectionItem>()
                .ForMember(dest => dest.EntitelmentId, opt => opt.MapFrom(src => src.entitlementid))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.displayname));

            c.CreateMap<DatasetItem, Model.DatasetSelectionItem>()
                .ForMember(dest => dest.DatasetKey, opt => opt.MapFrom(src => src.key))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.descriptor));

            //AAIC.API.CLient.Sage100.Mobile.Lib.Repository.Article.ArticleItem, Model.ArticleListItem
                c.CreateMap<AAIC.API.CLient.Sage100.Mobile.Lib.Repository.Article.ArticleItem, Model.ArticleListItem>()
               .ForMember(dest => dest.Artikelnummer, opt => opt.MapFrom(src => src.Artikelnummer))
               .ForMember(dest => dest.Matchcode, opt => opt.MapFrom(src => src.Matchcode))
               .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.key));
            
        });
        builder.Services.AddSingleton(typeof(IMapper), autoMapperConfig.CreateMapper());


        // Dialogs
        builder.Services.AddSingleton<WelcomePage>();
        builder.Services.AddSingleton<WelcomePageViewModel>();
        builder.Services.AddTransient<EntitlementSelectionPage>();
        builder.Services.AddTransient<EntitlementSelectionPageViewModel>();
        builder.Services.AddTransient<DataSetSelectionPage>();
        builder.Services.AddTransient<DataSetSelectionPageViewModel>();
        builder.Services.AddTransient<MainMenuPage>();
        builder.Services.AddTransient<MainMenuPageViewModel>();
        builder.Services.AddTransient<ArticleListPage>();
        builder.Services.AddTransient<ArticleListPageViewModel>();
        //builder.Services.AddTransient<PEMPage>();
        //builder.Services.AddTransient<PEMPageViewModel>();
            /*builder.Services.AddTransient<ArticleDetailPage>();
            builder.Services.AddTransient<ArticleDetailPageViewModel>();*/

        // Logger
        builder.Services.AddSingleton<IAppLogger, OutputWindowLogger>();

        // SageId
        builder.Services.AddSingleton<ITokenService, TokenService>();
        builder.Services.AddSingleton<ISageIdToken, SageIdToken>();
        builder.Services.AddSingleton<ISageIdClient, SageIdClient>();
        builder.Services.AddSingleton<ISageIdSettings, OpenId.SageIdMauiSettings>();
        builder.Services.AddSingleton<IdentityModel.OidcClient.Browser.IBrowser, OpenId.Browser>();

        // SageId - HTTP-Clients
        builder.Services.AddHttpClient("entitlementApi",
            client => { client.BaseAddress = new Uri("https://entitlement.sage.de/"); });
            //client => { client.BaseAddress = new Uri("https://PC-10:5493/"); });
            builder.Services.AddHttpClient("connectivityApi",
            client => { client.BaseAddress = new Uri("https://connectivity.sage.de/"); });
            //client => { client.BaseAddress = new Uri("https://PC-10:5493"); });

            return builder.Build();
    }
}
}