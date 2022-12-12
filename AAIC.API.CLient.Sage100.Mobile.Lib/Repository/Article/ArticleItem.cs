using System;
using System.Text.Json.Serialization;

namespace AAIC.API.CLient.Sage100.Mobile.Lib.Repository.Article
{
    public class ArticleItem
    {
        [JsonPropertyName("$key"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string key { get; set; }
        [JsonPropertyName("$url"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string url { get; set; }
        [JsonPropertyName("$updated"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime updated { get; set; }
        [JsonPropertyName("$descriptor"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string descriptor { get; set; }
        [JsonPropertyName("$etag"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string etag { get; set; }
        public object[] CustomFields { get; set; }
        public string Artikelnummer { get; set; }
        public string Bezeichnung1 { get; set; }
        public string Matchcode { get; set; }
        public string Artikelgruppe { get; set; }
        public int Steuerklasse { get; set; }
        public string Verkaufsmengeneinheit { get; set; }
        public int DezimalstellenVK { get; set; }
        public string Basismengeneinheit { get; set; }
        public int DezimalstellenBasis { get; set; }
        public string Lagermengeneinheit { get; set; }
        public int DezimalstellenLager { get; set; }
        public float UmrechnungsFaktorLME { get; set; }
        public int Lagerfuehrung { get; set; }
        public string Hauptartikelgruppe { get; set; }
        public string Vaterartikelgruppe { get; set; }
        public int Besteuerungsart { get; set; }
        public int AuspraegungID { get; set; }
        public string Langtext { get; set; }
        public string Bezeichnung2 { get; set; }
        public string Dimensionstext { get; set; }
        public int Erloescode { get; set; }
        public string Gewichtseinheit { get; set; }
        public string Hersteller { get; set; }
        public int IstBestellartikel { get; set; }
        public int IstRabattfaehig { get; set; }
        public int IstSkontierfaehig { get; set; }
        public int IstVerkaufsartikel { get; set; }
        public int PreiseinheitVK { get; set; }
        public float UmrechnungsfaktorVK { get; set; }
        public float Gewicht { get; set; }
        public float? GewichtLME { get; set; }
        public float KalkulatorischerEK { get; set; }
        public float Meldebestand { get; set; }
        public string EANNummer { get; set; }
    }
}