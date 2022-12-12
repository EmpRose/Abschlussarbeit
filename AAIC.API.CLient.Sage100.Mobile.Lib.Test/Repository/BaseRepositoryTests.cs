using Microsoft.VisualStudio.TestTools.UnitTesting;
using AAIC.API.CLient.Sage100.Mobile.Lib.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel.Client;
using IdentityModel.OidcClient.Browser;
using System.Diagnostics;
using AAIC.API.CLient.Sage100.Mobile.Lib.OpenId;
using AAIC.API.CLient.Sage100.Mobile.Lib.Token;
using AAIC.API.CLient.Sage100.Mobile.Lib.Exceptions;
using AAIC.API.CLient.Sage100.Mobile.Lib.Extensions;
using AAIC.API.CLient.Sage100.Mobile.Lib.Repository.Article;

//namespace AAIC.API.CLient.Sage100.Mobile.Lib.Repository.Tests
//{
    /*[TestClass()]
    public class BaseRepositoryTests  // Erstellung der Unit-Tests
    {
        // https://mp-srv:5493/sdata/ol/MasterData/PemCloud;123/recDokumente.106273525.AAI_S100_PEM('NA==;;;;;;;;')?include=$children&language=de
        // Aufruf "eines" Dokumentes
        // $url=https://mp-srv:5493/sdata/ol/MasterData/PemCloud;123/recProtokoll.106273525.AAI_S100_PEM('Mjc=')
        private const string CRUD_Key = "$NA==;;;;;;;;";
        //private const string CRUD_Artikelnummer = "00000001";
        //private const int CRUD_Auspraegung = 0;
        private const string RechnungAbacus = "$Mjc=";
        
        private const int entitlementId = 1037903;
        private const string dataset = "PemCloud;123";

        private BaseRepository<object,object> GetRepository() // Typparameter (object) werden im Verlauf durch Listen- und Entitäts-Klassen ersetzt
        {
            var httpFactory = new Test.OpenId.TestHttpClientFactory();
            var client = new SageIdClient(
                new TestSageIdSettings(),
                new TestBrowser(),
                new TestTokenService(),
                httpFactory);

            return new BaseRepository<object,object>(client, entitlementId, dataset,"apiDokumentenSchritte.Sage.API","eptDokumentenSchritte.Sage.API", httpFactory);
        }

        public Task<ISageIdToken> GetTokenAsync()
        {
            var token = new SageIdToken()
            {
                AccessToken = "ey...".ToSecureString(),
                Expiry = DateTime.Now,
                RefreshToken = "".ToSecureString()
            };
            return Task.FromResult(token as ISageIdToken);
        }
    */





















        /*
        [TestMethod()]
        public async Task GetListAsync()
        {
            var repo = GetRepository();
            var list = await repo.GetListAsync();
            Assert.IsNotNull(list);
            Assert.IsNotNull(list.resources);
            Assert.AreNotEqual(0, list.resources.Count);
        }
        // Um die Response auszuwerten, Haltepunkt auf "Asser.IsNotNull" setzen. Über das Visual Studio "Lokal"-Fenster kann die list-Variable ausgelesen werden



        // Zwei Tests für erfolgreiches "und" fehlerfreies Filtern.
        [TestMethod()]
        public async Task GetListFilterAsync()
        {
            var repo = GetRepository();
            var list = await repo.GetListAsync("(Artikelnummer eq '00200050')");
            Assert.IsNotNull(list);
            Assert.IsNotNull(list.resources);
            Assert.AreEqual(1, list.resources.Count);
            Assert.AreEqual("0200050", list.resources.First().Artikelnummer);
            Assert.AreEqual("Stehleuchte", list.resources.First().Bezeichnung1);
        }

        [TestMethod()]
        public async Task GetListFilterAsync_Error()
        {
            var repo = GetRepository();
            try
            {
                var list = await repo.GetListAsync("errir");
            }
            catch (HttpException e)
            {
                Debug.WriteLine(e.StatusCode);
                Debug.WriteLine(e.Message);
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public async Task GetTemplateAsync()
        {
            var repo = GetRepository();
            var item = await repo.GetTemplateAsync();
            Assert.IsNotNull(item);
            Assert.AreEqual(null, item.key);
            Assert.AreEqual("", item.Artikelnummer);
        }

        // Zwei Testmethoden für fehlerhaftes Abrufen
        [TestMethod()]
        public async Task GetAsync()
        {
            var repo = GetRepository();
            var item = await repo.GetAsync(StehleuchtKey);
            Assert.IsNotNull(item);
            Assert.AreEqual(StehleuchtKey, item.key);
            Assert.AreEqual("00200050", item.Artikelnummer);
            Assert.AreEqual("Stehleuchte", item.Bezeichnung1);
        }

        [TestMethod()]
        public async Task GetAsync_Error()
        {
            var repo = GetRepository();
            try
            {
                var item = await repo.GetAsync("");
                Assert.Fail();
            }
            catch (HttpException e)
            {
                Debug.WriteLine(e.StatusCode);
                Debug.WriteLine(e.Message);
                Assert.IsTrue(true);
            }
        }

        // Für den Test ist es wichtig, dass zunächst ein leerer Template-Datensatz abgerufen wird
        [TestMethod()]
        public async Task AddAsync()
        {
            var repo = GetRepository();
            var template = await repo.GetTemplateAsync();
            template.Artikelnummer = CRUD_Artikelnummer;
            template.AuspraegungID = CRUD_Auspraegung;

            template.Bezeichnung1 = "Test";
            template.Basismengeneinheit = "qm";
            template.Artikelgruppe = "010";
            template.Hauptartikelgruppe = "010";
            template.Vaterartikelgruppe = "EMPTY";

            var addResponse = await repo.AddAsync(template);
            Assert.IsNotNull(addResponse);
            Assert.AreEqual(template.Artikelnummer, addResponse.Artikelnummer);
            Assert.AreEqual(template.Artikelgruppe, addResponse.Artikelgruppe);
        }

        [TestMethod()]
        public async Task AddAsync_Error()
        {
            var repo = GetRepository();
            var template = await repo.GetTemplateAsync();
            template.Artikelnummer = CRUD_Artikelnummer;
            template.AuspraegungID = CRUD_Auspraegung;

            try
            {
                var addResponse = await repo.AddAsync(template);
                Assert.Fail();
            }
            catch (HttpException e)
            {
                Debug.WriteLine(e.StatusCode);
                Debug.WriteLine(e.Message);
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public async Task EditAsync()
        {
            var repo = GetRepository();
            var item = await repo.GetAsync(CRUD_Key);
            Assert.IsNotNull(item);
            item.Bezeichnung1 += "Edit";
            var editResult = await repo.EditAsync(item, item.key, item.etag);
            Assert.IsNotNull(editResult);
        }

        [TestMethod]
        public async Task EditAsync_BadKey()
        {
            var repo = GetRepository();
            var item = await repo.GetAsync(CRUD_Key);
            Assert.IsNotNull(item);
            item.Bezeichnung1 += "Edit";
            try
            {
                var editResult = await repo.EditAsync(item, "", item.etag);
                Assert.Fail();
            }
            catch (HttpException e)
            {
                Debug.WriteLine(e.StatusCode);
                Debug.WriteLine(e.Message);
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public async Task EditAsync_BadIfMatch()
        {
            var repo = GetRepository();
            var item = await repo.GetAsync(CRUD_Key);
            Assert.IsNotNull(item);
            item.Bezeichnung1 += "Edit";
            try
            {
                var editResult = await repo.EditAsync(item, item.key, "");
                Assert.Fail();
            }
            catch (HttpException e)
            {
                Debug.WriteLine(e.StatusCode);
                Debug.WriteLine(e.Message);
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public async Task DeleteAsync()
        {
            var repo = GetRepository();
            var item = await repo.GetAsync(CRUD_Key);
            Assert.IsNotNull(item);
            var deleteResult = await repo.DeleteAsync(item.key);
            Assert.IsTrue(deleteResult);
        }

        [TestMethod]
        public async Task DeleteAsync_Fail()
        {
            var repo = GetRepository();
            var item = await repo.GetAsync(StehleuchtKey);
            Assert.IsNotNull(item);
            var deleteResult = await repo.DeleteAsync(item.key + "error");
            Assert.IsFalse(deleteResult);
        }

        [TestMethod()]
        public void BaseRepositoryTest()
        {
            Assert.Fail();
        }*/
    //}
//}