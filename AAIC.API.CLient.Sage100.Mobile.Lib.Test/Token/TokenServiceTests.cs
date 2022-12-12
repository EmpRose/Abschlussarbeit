using Microsoft.VisualStudio.TestTools.UnitTesting;
using AAIC.API.CLient.Sage100.Mobile.Lib.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAIC.API.CLient.Sage100.Mobile.Lib.Extensions;
namespace AAIC.API.CLient.Sage100.Mobile.Lib.Token.Tests
{
    [TestClass()]
    public class TokenServiceTests
    {
        /*[TestMethod()]                                 - Diese Test Methode wird nicht benötigt
         public void TokenServiceTest()
         {
             Assert.Fail();
         }*/


        // Wenn in einer Test-Methode "await" genutzt werden möchte, so muss die MEthodensignatur als "async Task" definiert werden

        [TestMethod()]
        public async Task SaveTokenAsyncTest()
        {
            var tokenService = new TokenService();                      // neues Objekt der Klasse TokenService
            var token = new SageIdToken()                               // neues Objekt der Klasse SageIdToken
            {
                AccessToken = "AccessToken".ToSecureString(),           // Übergebe Werte für das SageIdToken und bringe es ToSecureString
                RefreshToken = "RefreshToken".ToSecureString(),
                Expiry = DateTime.Now
            };
            var result = await tokenService.SaveTokenAsync(token);      // tokenService erhält token mit Inhalt und speicher in "result"
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public async Task DeleteTokenTest()
        {
            var tokenService = new TokenService();
            tokenService.DeleteToken();
            var token = await tokenService.GetTokenAsync();
            Assert.IsNull(token);
        }

        [TestMethod()]
        public async Task GetTokenAsyncTest()
        {
            var tokenService = new TokenService();                      // neues Objekt von Klasse TokenService
            var token = await tokenService.GetTokenAsync();             // übergibt Inhalt an token
            Assert.IsNotNull(token);                                    // überprüft auch wenn "leer" nur nicht "null"
        }

        [TestMethod()]
        public async Task SaveAndGetTokenAsync()
        {
            var tokenService = new TokenService();
            tokenService.DeleteToken();
            var token = new SageIdToken()
            {
                AccessToken = "AccessToken".ToSecureString(),
                RefreshToken = "RefreshToken".ToSecureString(),
                Expiry = DateTime.Now
            };

            var result = await tokenService.SaveTokenAsync(token);
            Assert.IsTrue(result);
        }
    }
}