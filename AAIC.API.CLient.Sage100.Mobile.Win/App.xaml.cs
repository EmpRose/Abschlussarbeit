using AAIC.API.CLient.Sage100.Mobile.Lib.OpenId;
using AAIC.API.CLient.Sage100.Mobile.Lib.Token;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AAIC.API.CLient.Sage100.Mobile.Win
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost host;
        

        public App()
        {
            host = new HostBuilder().ConfigureServices((host, services) =>  // Initialisation eines Programms mit dem Objekt "host"
                                                                            // und fügt dem Container "host" Dienste hinzu, die auch mehrfach gebraucht werden können "services"
                {
                services.AddHttpClient("entitlementApi",                    // Bei der Nutzung/Erstellung des Names "entitlementApi" - verweise auf Standard URL dafür
                    Client =>
                    {
                        Client.BaseAddress = new Uri("https://entitlement.sage.de");
                    });

                services.AddHttpClient("connectivityApi",                   // Bei der Nutzung/Erstellung des Names "connectivityApi" - verweise auf Standard URL dafür
                    Client =>
                    {
                        Client.BaseAddress = new Uri("https://connectivity.sage.de");
                    });

                services.AddSingleton<ITokenService, TokenService>();
                services.AddSingleton<ISageIdToken, SageIdToken>();
                services.AddSingleton<ISageIdClient, SageIdClient>();
                services.AddSingleton<ISageIdSettings, OpenId.SageIdDesktopSettings>();
                services.AddSingleton<IdentityModel.OidcClient.Browser.IBrowser, OpenId.EmbeddedBrowser>();
                services.AddSingleton<MainWindow>();

            }).Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await host.StartAsync();

            var mainwindow = host.Services.GetRequiredService<MainWindow>();
            mainwindow.Show();

            base.OnStartup(e);
        }
    }
}
