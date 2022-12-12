using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAIC.API.CLient.Sage100.Mobile.Lib.Test.OpenId
{
    internal class TestHttpClientFactory : IHttpClientFactory
    {
        public HttpClient CreateClient(string name)                         // Erstellt einen HttpClient und übergibt anhand des erhaltenen Namens die richtige oder keine URL zurück
        {
            if (name == "entitlementApi")
                return new HttpClient()
                {
                    BaseAddress = new Uri("https://entitlement.sage.de/")
                };

            if (name == "connectivityApi")
                return new HttpClient()
                {
                    BaseAddress = new Uri("https://connectivity.sage.de/")
                };
            return null;

        }
    }
}
