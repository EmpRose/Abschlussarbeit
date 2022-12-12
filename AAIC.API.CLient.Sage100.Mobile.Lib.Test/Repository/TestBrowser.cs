using IdentityModel.OidcClient.Browser;

namespace AAIC.API.CLient.Sage100.Mobile.Lib.Repository.Tests
{
    public class TestBrowser : IBrowser
    {
        public Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}