using AAIC.API.CLient.Sage100.Mobile.Lib.Token;
using System;
using System.Collections.Generic;
using System.Text;
using IdentityModel.OidcClient.Browser;
using System.Net.Http;
using IdentityModel.OidcClient;
using System.Threading.Tasks;
using AAIC.API.CLient.Sage100.Mobile.Lib.Model;
using System.Runtime.CompilerServices;
using AAIC.API.CLient.Sage100.Mobile.Lib.Extensions;
using IdentityModel.Client;
using System.Net.Http.Headers;
using IdentityModel;
using System.Text.Json.Serialization;
using AAIC.API.CLient.Sage100.Mobile.Lib.Exceptions;
using System.Net.Http.Json;
using System.Linq;

namespace AAIC.API.CLient.Sage100.Mobile.Lib.OpenId
{
    public class SageIdClient : ISageIdClient
    {
        private readonly ISageIdSettings sageIdSettings;            // Erstellen des Konstruktors und Abhängigkeiten die wir benötigen
        private readonly IBrowser browser;
        private readonly ITokenService tokenService;
        private readonly IHttpClientFactory httpClientFactory;
        

        public SageIdClient(ISageIdSettings sageIdSettings, IBrowser browser, ITokenService tokenService, IHttpClientFactory httpClientFactory)
        {
            this.sageIdSettings = sageIdSettings;
            this.browser = browser;
            this.tokenService = tokenService;
            this.httpClientFactory = httpClientFactory;
            
        }

        // Es werden Methoden wie Login, RefreshLogin, Logout, Berechtigung erhalten, Datenpart erhalten benötigt

        private OidcClientOptions GetOidcClientOptions()
        {
            return new OidcClientOptions
            {
                Authority = sageIdSettings.Authority,
                ClientId = sageIdSettings.ClientId,
                Scope = sageIdSettings.Scope,                               // Gültigkeitsbereich
                RedirectUri = sageIdSettings.RedirectUri,
                Browser = browser
            };
        }

        /*
         * Die Login-Methode greift zunächst auf den TokenService zu, 
         * falls im TokenService ein Token gefunden wird und dieses AccessToken nicht abgelaufen ist, 
         * wird dieser direkt zurückgegeben. Falls der AccessToken bereits seine Gültigkeit verloren hat, 
         * wird versucht einen neuen Token anhand des RefeshTokens zu ermitteln.
         * */
        public async Task<SageIdRequestResult> LoginAsync()
        {
            // Erstellen einer Token Variablen
            var token = await tokenService.GetTokenAsync();
            // Abfrage ob ein Token vorhanden ist (not null von return)
            if (token != null)
            {   // Ist der Token noch gültig? Sobald das Ablaufdatum größer ist als UtcNow, können wir den Token ansprechen.
                if (token.Expiry > DateTime.UtcNow)
                {
                    return new SageIdRequestResult()
                    {
                        IsError = false, // ist Fehler vorhanden?
                        Error = "", // Fehler
                        Token = token // Token deklaration
                    };
                }
                //Enthält unser Token einen RefreshToken? (RefreshToken not null)
                else if (token.RefreshToken != null)
                {
                    SageIdRequestResult refreshResult = await RefreshAccessTokenAsync(token);
                    if (!refreshResult?.IsError ?? false)
                        return refreshResult;
                }
            }

            var options = GetOidcClientOptions();
            var oidcClient = new OidcClient(options);
            var loginResult = await oidcClient.LoginAsync(new LoginRequest()
            {
                FrontChannelExtraParameters = sageIdSettings.FrontChannelExtraParameters
            });

            var sageIdResult = new SageIdRequestResult()
            {
                IsError = loginResult.IsError,
                Error = loginResult.Error,
                Token = new SageIdToken()
                {//Inhalt des SageIdToken
                    AccessToken = loginResult.AccessToken.ToSecureString(),
                    RefreshToken = loginResult.RefreshToken.ToSecureString(),
                    Expiry = loginResult.AccessTokenExpiration.DateTime.ToUniversalTime()
                }
            };

            await tokenService.SaveTokenAsync(sageIdResult.Token);
            return sageIdResult;
        }



        public async Task<SageIdRequestResult> RefreshAccessTokenAsync(ISageIdToken sageIdToken)
        {
            var options = GetOidcClientOptions();                           // fordert neue Parameter an und übernimmt diese in "options"
            var oidcClient = new OidcClient(options);                       // erstellt das Object oidcClient der Klasse OidcClient 
            var refreshResult = await oidcClient.RefreshTokenAsync(         // "erfrischt" den "RefreshToken" nach dem dieser "unsecure" gemacht wurde , übegibt Parameters
                sageIdToken.RefreshToken.FromSecureString(),
                sageIdSettings.FrontChannelExtraParameters);

                                                                            //Nachdem ein neuer AccessToken ermittelt wurde,
                                                                            //wird dieser wieder abgespeichert und an die aufrufende Stelle zurpckgegeben
            var SageIdResult = new SageIdRequestResult()                    // Objekt der Klasse SageIdRequestResult generiert
            {
                IsError = refreshResult.IsError,                            // Fehlerinhalt übergeben aus der Variablen mit der "erfrischten" Information
                Error = refreshResult.Error,
                Token = new SageIdToken()                                   // Erstellt einen neues Tokenobjekt (Inkl. AccessToken,RefreshToken und Expiry)
                {
                    AccessToken = refreshResult.AccessToken.ToSecureString(),
                    RefreshToken = refreshResult.RefreshToken.ToSecureString(),
                    Expiry = refreshResult.AccessTokenExpiration.DateTime.ToUniversalTime()
                }
            };
            if (!SageIdResult.IsError)                                      // Wenn kein Fehler mit übergeben wurde
                await tokenService.SaveTokenAsync(SageIdResult.Token);      // Warte bis der neu generierte Token gespeichert wird

            return SageIdResult;                                            // gebe den neuen/erschrischten Token zurück
        }

        public async Task<bool> LogoutAsync()
        {
            var startUrl = new RequestUrl(sageIdSettings.Authority +        // Erzeugt eine neue Request/Anfrage mit der Info von Authority , ClientId sowie Redirect Url
                "v2/logout").Create(new Parameters(new Dictionary<string, string>
            {
            {"client_id",sageIdSettings.ClientId},
            { "returnTo",sageIdSettings.RedirectUri}
        }));

            var result = await browser.InvokeAsync(new BrowserOptions       // Warte auf Antwort des Browsers mit übergabe der zuvor erhaltenen Informationen als Klasse
                (startUrl, sageIdSettings.RedirectUri));
            if (result.ResultType == BrowserResultType.Success)             // Wenn das Resultat identisch der Antwort ist (was immer so sein sollte)
                tokenService.DeleteToken();                                 // lösche den vorhandenen Token

            return result.ResultType == BrowserResultType.Success;          // Gebe bestätigung über erfolgreiche Ausführung zurück


        }

        public async Task<IEnumerable<Entitlement>> GetEntitlementAsync(ISageIdToken sageIdToken)   // Nach der erstellung der Klassen Entitlement und Entitlements
                                                                                                    // wird Tast<IEnumerable<objet>> geändert "objet zu Entitlement"
                                                                                                    // Verweist die Methode auf die Liste "Entitlements"
        {
            var client = httpClientFactory.CreateClient("entitlementApi");                          // erstellt einen neuen HttpClient mit dem Namen "entitlementApi"                 
            client.DefaultRequestHeaders.Accept.Add(                                                // Fügt Informationen über Header Requests zu erwarteten Sprache application/json
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization =                                            // Fügt Informationen über Header Requests zur Art der Anmeldung "Bearer" und übergibt
                                                                                                    // den entschlüsselten AccessToken
                new AuthenticationHeaderValue(
                    OidcConstants.AuthenticationSchemes.AuthorizationHeaderBearer,
                    sageIdToken.AccessToken.FromSecureString());

            var response = await client.GetAsync(                                                   // neue Anfrage auf die vorgegebene URL
                "sdata/entitlementWS/entitlementContract10/-/entitlements");
                
            if (!response.IsSuccessStatusCode)                                                      // Wenn die Antwort nicht "Erfolgreich/zBsp 200 ist"
            {
                /*Nur bis zurKonfiguration von HttpException benötigt um
                Fehler in Schnellansicht auszulesen und JSON zu erstellen */
                /*var error = await response.Content.ReadAsStringAsync();
                return null;*/                                                                      // Versuche die Antwort vom Server in einer Zeichenkette auszulesen und anzuzeigen
                throw new HttpException(response.StatusCode, await response.Content.ReadAsStringAsync());
            }
            var entitlements = await response.Content.ReadFromJsonAsync<Entitlements>();            // Berechtigungen werden anhand der "Entitlements JSON Liste" erwartet und übergeben
            return entitlements.resources.                                                          // Erteile Rechte anhand des Matchcodes aus der "Connectivity_Sage_OL_DE" erhalten
            Where(e => e.servicematchcode == "Connectivity_Sage_OL_DE");

        }

        public async Task<DatasetResponse> GetDatasetsAsync(                                        // Verweist die Methode auf die Liste "DatasetResponse"
            ISageIdToken sageIdToken, int entitlementid)                                            // Empfängt den IdToken und die Berechtigungsnummer
                                                                                                    // (ich nutze meine eigene aus der Anmeldung bei Sage100 mit dem Sage Account m.pospelov@aaic.de)
        {
            var client = httpClientFactory.CreateClient("connectivityApi");                         // Erstellt einen neuen HttpClient mit dem Namen "connectivityAPI"
            client.DefaultRequestHeaders.Add("X-Sage-ConnectivityVersion", "1.3");                  // Gibt Information über die Art der Verbindung im Header mit
                                                                                                    // Sage erwartet immer die ConnectivityVersion 1.3, dass ist Pflicht
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(            // Gibt Informationen über die erwartete Sprache zur Kommunikation mit. application/json
                "application/json"));

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(             // Gibt die Art des Autorisierung vor "Baerer" und liefert AccessToken nach der "entschlüsselung" mit
                OidcConstants.AuthenticationSchemes.AuthorizationHeaderBearer,
                sageIdToken.AccessToken.FromSecureString());
                                                                                                    // Es wird ein GET Request versendet (an die Sage API-Schnittstelle)
                                                                                                    // und eine Antwort vom Server erwartet
            var response = await client.GetAsync($"ws/{entitlementid}/sdata/ol/apiKunden.Sage.API");
            //var response = await client.GetAsync($"{entitlementid}/sdata/ol/apiKunden.Sage.API");

            if (!response.IsSuccessStatusCode)                                                      // Wenn der Server nicht "positiv" antwortet
            {
                //Nur bis zurKonfiguration von HttpException benötigt um Fehler in Schnellansicht auszulesen und JSON zu erstellen
                //var error = await response.Content.ReadAsStringAsync();
                //return null;                                                                      // Die Antwort in einer Zeichenkette darzustellen
                throw new HttpException(response.StatusCode, await response.Content.ReadAsStringAsync());
            }
            return await response.Content.ReadFromJsonAsync<DatasetResponse>();                     // Erwarte die Antwort vom Server anhand der JSON DatasetResponse Liste

        }
    }
}













