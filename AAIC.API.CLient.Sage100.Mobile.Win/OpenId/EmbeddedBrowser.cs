using IdentityModel.OidcClient.Browser;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace OpenId
{
    internal class EmbeddedBrowser : IBrowser               // Abgeleitet von Existierendem Interface 
    {
        public async Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
        {
            var semaphoreSlim = new SemaphoreSlim(0, 1); // Beschränkt die Anzahl der Threads auf 1 und fängt mit der 0 an.
            var browserResult = new BrowserResult()
            {
                ResultType = BrowserResultType.UserCancel
            };

            var signinWindow = new Window()             // erschaft ein neues Windowsfenster "signinWindow"
            {
                Title = "Sage ID Anmeldung",
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            signinWindow.Closing += (s, e) => // LESEN
            {
                semaphoreSlim.Release();                // Gibt das Semaphore Objekt einmal frei
            };

            var webView = new WebView2();
            webView.NavigationStarting += (s, e) =>     // Navigation zur einer URL wird angefordert
            {                                           
                if (new Uri(e.Uri).AbsoluteUri.StartsWith(options.EndUrl))
                                                        //Die AbsoluteUri Eigenschaft enthält den gesamten in der Uri Instanz gespeicherten URI,
                                                        //einschließlich aller Fragmente und Abfragezeichenfolgen.
                {
                    e.Cancel = true;
                    browserResult = new BrowserResult()
                    {
                        ResultType = BrowserResultType.Success,
                        Response = new Uri(e.Uri).AbsoluteUri
                    };
                    semaphoreSlim.Release();           // Gibt das Semaphore Objekt einmal frei
                    signinWindow.Close();               
                }
            };                                                      
            signinWindow.Content = webView;
            signinWindow.Show();

            await webView.EnsureCoreWebView2Async(null);            //Blockiert die Navigation bis webView fertig ist
            webView.CoreWebView2.CookieManager.DeleteAllCookies();  // Bereinigt die erhalteten Cookies
            webView.CoreWebView2.Navigate(options.StartUrl);        //

            await semaphoreSlim.WaitAsync();            // Abwarten und Tee trinken, bis der Thread(SemaphoreSlim) fertig ist.

            return browserResult;

        }
    }
}
