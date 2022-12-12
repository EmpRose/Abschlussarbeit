using Microsoft.VisualStudio.TestTools.UnitTesting;
using AAIC.API.CLient.Sage100.Mobile.Lib.OpenId;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAIC.API.CLient.Sage100.Mobile.Lib.Test.OpenId;
using AAIC.API.CLient.Sage100.Mobile.Lib.Token;
using AAIC.API.CLient.Sage100.Mobile.Lib.Extensions;

namespace AAIC.API.CLient.Sage100.Mobile.Lib.OpenId.Tests
{
    [TestClass()]
    public class SageIdClientTests
    {
        [TestMethod()]
        public async Task SageIdClientTest()
        {
            var client = new SageIdClient(null,null,null, new TestHttpClientFactory());
            Assert.IsNotNull(client);
        }

        [TestMethod()]
        public void LoginAsyncTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RefreshAccessTokenAsyncTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public async Task LogoutAsyncTest()
        {
            var tokenService = new TokenService();                      // neues Objekt von Klasse TokenService
            var token = await tokenService.GetTokenAsync();
            Assert.IsNotNull(token);
            tokenService.DeleteToken();
            Assert.IsNull(token);
        }

        [TestMethod()]
        public async Task GetEntitlementAsyncTest()
        {
            var client = new SageIdClient(null, null, null, new TestHttpClientFactory());
            var token = new SageIdToken()
            {                   // Aus der Wpf.App (einer App der Sage 100) nach der Anmeldung mit meinem SageID Konto erhalten, kopiert und eingefügt (8h gültig)
                AccessToken = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6Ik56TXdPVVJHTVVZNU5ERXpRelJHUVVNMVF6azJSa1U1UVRJMU0wRTROemhGUmpWQ04wSTNOQSJ9.eyJpc3MiOiJodHRwczovL2lkLnNhZ2UuY29tLyIsInN1YiI6ImF1dGgwfGNiNDI3Y2Q0MWJjOWEyOTY5ZTk2MzhjZmNlNGM1MzdlYTE4OWUxMTMwNGRmNzcxYyIsImF1ZCI6WyJzMTAwZGUvc2FnZTEwMCJdLCJpYXQiOjE2NjYwMTI0NzAsImV4cCI6MTY2NjA0MTI3MCwiYXpwIjoibllTb1VWRExwQk9qdHN0Q2Exb3NocndNMEpXaVFKdUwiLCJzY29wZSI6Im9wZW5pZCBwcm9maWxlIGVtYWlsIG9mZmxpbmVfYWNjZXNzIn0.LkKToHMDGK_-LvAE69rv85N6w3NN-p8n1TRYfmhoxAnNVAq_FW3KOR8fId0X4p6km7Ab1qxH3NKG7pbGCkQSvI7yHvWewUSobjRZg1s9vSxLfY1tWlzs13ZnS1mNqy2MMVW2vF3JY9Xt8tXE_EOsQCCLn5XLS4e3SCtJwRDU3XHm7HHh9ep3n_6ANCwfnbg6K4QIEDngZkzPbN-Hyj_Abl6LpVVRlJhn0pL6Y0g0i7CH9x1Ucduq5sahhQZNAdJUuksAC3hxTzljPvmrrYKQwx28_X-d7aDHaIeKj8hlM8IZoj5yOg1JQPSPtTsUeQ4FwqLvkDeOiiuLDrQiA2-DEg".ToSecureString()
            };
            var result = await client.GetEntitlementAsync(token);
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public async Task GetDataSetsAsyncTest()
        {
            var client = new SageIdClient(null, null, null, new TestHttpClientFactory());
            var token = new SageIdToken()
            {
                AccessToken = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6Ik56TXdPVVJHTVVZNU5ERXpRelJHUVVNMVF6azJSa1U1UVRJMU0wRTROemhGUmpWQ04wSTNOQSJ9.eyJpc3MiOiJodHRwczovL2lkLnNhZ2UuY29tLyIsInN1YiI6ImF1dGgwfGNiNDI3Y2Q0MWJjOWEyOTY5ZTk2MzhjZmNlNGM1MzdlYTE4OWUxMTMwNGRmNzcxYyIsImF1ZCI6WyJzMTAwZGUvc2FnZTEwMCJdLCJpYXQiOjE2NjYwMTI0NzAsImV4cCI6MTY2NjA0MTI3MCwiYXpwIjoibllTb1VWRExwQk9qdHN0Q2Exb3NocndNMEpXaVFKdUwiLCJzY29wZSI6Im9wZW5pZCBwcm9maWxlIGVtYWlsIG9mZmxpbmVfYWNjZXNzIn0.LkKToHMDGK_-LvAE69rv85N6w3NN-p8n1TRYfmhoxAnNVAq_FW3KOR8fId0X4p6km7Ab1qxH3NKG7pbGCkQSvI7yHvWewUSobjRZg1s9vSxLfY1tWlzs13ZnS1mNqy2MMVW2vF3JY9Xt8tXE_EOsQCCLn5XLS4e3SCtJwRDU3XHm7HHh9ep3n_6ANCwfnbg6K4QIEDngZkzPbN-Hyj_Abl6LpVVRlJhn0pL6Y0g0i7CH9x1Ucduq5sahhQZNAdJUuksAC3hxTzljPvmrrYKQwx28_X-d7aDHaIeKj8hlM8IZoj5yOg1JQPSPtTsUeQ4FwqLvkDeOiiuLDrQiA2-DEg".ToSecureString()
            };
            var result = await client.GetDatasetsAsync(token, 1037903); // Entitlement ID zu finden im Sage Server Manager
                                                                        // nach Anmeldung (Konfiguration externer API)
            Assert.IsNotNull(result);
        }
    }
}