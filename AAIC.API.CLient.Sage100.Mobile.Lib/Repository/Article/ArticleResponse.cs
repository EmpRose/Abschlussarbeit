using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AAIC.API.CLient.Sage100.Mobile.Lib.Repository.Article
{
    public class ArticleResponse
    {
        [JsonPropertyName("$url")]
        public string url { get; set; }
        [JsonPropertyName("$descriptor")]
        public string descriptor { get; set; }
        [JsonPropertyName("$resources")]
        public List<ArticleItem> resources { get; set; }
    }
}