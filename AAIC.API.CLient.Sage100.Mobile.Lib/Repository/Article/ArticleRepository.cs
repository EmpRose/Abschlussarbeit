using AAIC.API.CLient.Sage100.Mobile.Lib.OpenId;
using AAIC.API.CLient.Sage100.Mobile.Lib.Repository;
using AAIC.API.CLient.Sage100.Mobile.Lib.Repository.Article;
using System.Net.Http;

namespace AAIC.API.CLient.Sage100.Mobile.Lib.Repository.Article
{
    public class ArticleRepository : BaseRepository<ArticleResponse, ArticleItem>
    {
        /*
         * Um den Aufruf des Repositories zu vereinfachen, wird ArticleRepository erstellt. 
         * Leitet vom BaseRepository ab und definiert ArticleResponse und Article.
         * Beim Aufruf des Konstruktors werden die API-URL-Properties mit definiert
         */
        public ArticleRepository(
            ISageIdClient sageIdClient, int entitlementId, string dataset, IHttpClientFactory httpClientFactory)
            : base(sageIdClient, entitlementId, dataset, "apiArtikel.Sage.API", "eptArtikel.Sage.API", httpClientFactory)
        {
        }
    }
}
