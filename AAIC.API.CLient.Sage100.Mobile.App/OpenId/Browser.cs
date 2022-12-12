using AAIC.API.CLient.Sage100.Mobile.App.Logger;
using IdentityModel.OidcClient.Browser;



namespace AAIC.API.CLient.Sage100.Mobile.App.OpenId
{
    internal class Browser : IdentityModel.OidcClient.Browser.IBrowser
    {
        private readonly IAppLogger logger;
        public Browser(IAppLogger logger)
        {
            this.logger = logger;
        }

        public async Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
        {
            try
            {
                WebAuthenticatorResult authResult = null;

#if WINDOWS
                authResult = await WinUIEx.WebAuthenticator.AuthenticateAsync(new Uri(options.StartUrl), // "file" zum Auslesen in der "FileTokenService"-Klasse für die WinApp
                    new Uri("myprot://myapp.com/callback"));
#else
                
                //authResult = await WebAuthenticator.AuthenticateAsync(new Uri(options.StartUrl),    // Wo kommt die StartUrl her, wenn diese nur im "Test" des HttpClientFactory angegeben wurde
                //    new Uri("myprot://myapp.com/callback"));                                        // Callback Uri ist angegeben , liegt es also an der StartUri?
                                                                                                    // In SageMauiSettings ist Authority angegeben "https://id.sage.com/"
               

                
                var url = new Uri(options.StartUrl);
                var callbackUrl = new Uri("myprot://myapp.com/callback");
                

                authResult = await WebAuthenticator.AuthenticateAsync(new WebAuthenticatorOptions
                {
                    Url = url,
                    CallbackUrl = callbackUrl, 
                });
                

#endif

                var authorizeResponse = ToRawIdentityUrl(options.EndUrl, authResult);

                return new BrowserResult
                {
                    Response = authorizeResponse
                };
            }
            catch (TaskCanceledException ex)
            {
                logger.Error(ex);
                return new BrowserResult
                {
                    ResultType = BrowserResultType.UserCancel,
                    Error = ex.ToString()
                };
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return new BrowserResult()
                {
                    ResultType = BrowserResultType.UnknownError,
                    Error = ex.ToString()
                };
            }
        }

        public string ToRawIdentityUrl(string redirectUrl, WebAuthenticatorResult result)
        {
            try
            {
                IEnumerable<string> parameters = result.Properties.Select(pair => $"{pair.Key}={pair.Value}");
                var modifiedParameters = parameters.ToList();

                var stateParameter = modifiedParameters
                    .FirstOrDefault(p => p.StartsWith("state", StringComparison.OrdinalIgnoreCase));

                if (!string.IsNullOrWhiteSpace(stateParameter))
                {
                    // Remove the state key added by WebAuthenticator that includes appInstanceId
                    modifiedParameters = modifiedParameters.Where(p => !p.StartsWith("state", StringComparison.OrdinalIgnoreCase)).ToList();

                    stateParameter = System.Web.HttpUtility.UrlDecode(stateParameter).Split('&').Last();
                    modifiedParameters.Add(stateParameter);
                }
                var values = string.Join("&", modifiedParameters);
                return $"{redirectUrl}#{values}";
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw;
            }
        }
    }
}
