using AAIC.API.CLient.Sage100.Mobile.Lib.OpenId;
using IdentityModel;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AAIC.API.CLient.Sage100.Mobile.Lib.Extensions;
using AAIC.API.CLient.Sage100.Mobile.Lib.Token;
using AAIC.API.CLient.Sage100.Mobile.Lib.Exceptions;
using System.Net.Http.Json;
using System.Net.Http.Headers;

namespace AAIC.API.CLient.Sage100.Mobile.Lib.Repository // Erstellung der Repositories
{
    public class BaseRepository<TList, T>
        where TList : class                     // Die Parameter TList und T werden bei der späteren Verwendung mit
        where T : class                         // konkreten Klassen für die Liste und einzelne Entitäten beschrieben

    {
        private readonly ISageIdClient sageIdClient;
        private readonly int entitlementId;
        private readonly string dataset;
        private readonly string api;
        private readonly string endpoint;
        private readonly IHttpClientFactory httpClientFactory;

        public BaseRepository(
            ISageIdClient sageIdClient,
            int entitlementId,
            string dataset,
            string api,
            string endpoint,
            IHttpClientFactory httpClientFactory)
        {
            this.sageIdClient = sageIdClient;
            this.entitlementId = entitlementId;
            this.dataset = dataset;
            this.api = api;
            this.endpoint = endpoint;
            this.httpClientFactory = httpClientFactory;
        }

        private async Task<HttpClient> PrepairClient(string ifMatch = "")
        {
            var loginResult = await sageIdClient.LoginAsync();
            if (loginResult.IsError)
                throw new HttpException(HttpStatusCode.Unauthorized, "Error at SageId Login");

            var client = httpClientFactory.CreateClient("connectivityApi");
            client.DefaultRequestHeaders.Add("X-Sage-ConnectivityVersion", "1.3");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                OidcConstants.AuthenticationSchemes.AuthorizationHeaderBearer,
                loginResult.Token.AccessToken.FromSecureString());

            if (!string.IsNullOrWhiteSpace(ifMatch))
                client.DefaultRequestHeaders.TryAddWithoutValidation("if-match", ifMatch);

            return client;
        }
        // Die Routen der APIs werden mit drei einzelnen Methoden abgebildet.

        private string GetRequestUrl() => $"ws/{entitlementId}/sdata/ol/{api}/{dataset}/{endpoint}";
        //private string GetRequestUrl() => $"{entitlementId}/sdata/ol/{api}/{dataset}/{endpoint}";

        private string GetRequestUrlWhithKey(string key) => $"ws/{entitlementId}/sdata/ol/{api}/{dataset}/{endpoint}('{key}')";

        private string GetRequestUrlWithFilter(string filter) => $"ws/{entitlementId}/sdata/ol/{api}/{dataset}/{endpoint}?where={System.Uri.EscapeDataString(filter)}";


        // Mit der GetListAsync Methode wird direkt der Endpunkt angesprochen und die Liste der Datensätze abgerufen
        public async Task<TList> GetListAsync()
        {
            var client = await PrepairClient();
            var response = await client.GetAsync(GetRequestUrl());
            if (!response.IsSuccessStatusCode)
                throw new HttpException(response.StatusCode, await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<TList>();
        }


        // Für eine Filtermöglichkeit erfolg die Überladung der GetListAsync Methode
        public async Task<TList> GetListAsync(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
                return await GetListAsync();

            var client = await PrepairClient();

            var response = await client.GetAsync(GetRequestUrlWithFilter(filter));
            if (!response.IsSuccessStatusCode)
                throw new HttpException(response.StatusCode, await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<TList>();
        }

        // Mit der Template-Methode wird ein leerer Datensatz von Server geliefert, welcher dann im Rahmen einer Neuanlage genutzt werden sollte

        public async Task <T> GetTemplateAsync()
        {
            var client = await PrepairClient();
            var response = await client.GetAsync(GetRequestUrl() + "/$template");
            if (!response.IsSuccessStatusCode)
                throw new HttpException(response.StatusCode, await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<T>();
        }

        // Mit der GetAsync Methode, kann ein bestimmter Datensatz anhand des Schlüssels abgerufen werden.
        public async Task<T> GetAsync(string key)
        {
            var client = await PrepairClient();
            var response = await client.GetAsync(GetRequestUrlWhithKey(key));
            if (!response.IsSuccessStatusCode)
                throw new HttpException(response.StatusCode, await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<T>();
        }

        // Mit der AddAsync Methode kann ein neuer Datensatz angelegt werden.
        public async Task<T> AddAsync(T entity)
        {
            var client = await PrepairClient();
            var httpContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(entity),
                System.Text.Encoding.UTF8, "application/json");

            var response = await client.PostAsync(GetRequestUrl(), httpContent);
            if (!response.IsSuccessStatusCode)
                throw new HttpException(response.StatusCode, await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<T>();
        }

        //Mit EditAsync Methode kann ein Datensatz bearbeitet werden.
        public async Task<T> EditAsync(T entity, string key, string ifMatch)
        {
            var client = await PrepairClient(ifMatch);
            var httpContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(entity),
             System.Text.Encoding.UTF8, "application/json");

            var response = await client.PutAsync(GetRequestUrlWhithKey(key), httpContent);
            if (!response.IsSuccessStatusCode)
                throw new HttpException(response.StatusCode, await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<bool> DeleteAsync(string key)
        {
            var client = await PrepairClient();
            var response = await client.DeleteAsync(GetRequestUrlWhithKey(key));
            return response.IsSuccessStatusCode;
        }
    }
}
