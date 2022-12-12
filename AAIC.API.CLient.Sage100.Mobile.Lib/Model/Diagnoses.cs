
using System.Text.Json.Serialization;

public class Diagnoses

// Für jede Property muss das jeweilige JSON-Attribut "JsonPropertyName" hinzugefügt werden [JsonPropertyName("$NameVomProperty")]
{
    [JsonPropertyName("$severity")]
    public string Dseverity { set { severity = value; } }

    [JsonPropertyName("$sdataCode")]
    public string DsdataCode { set { sdataCode = value; } }

    [JsonPropertyName("$applicationCode")]
    public string DapplicationCode { set { applicationCode = value; } }

    [JsonPropertyName("$message")]
    public string Dmessage { set { message = value; } }

    [JsonPropertyName("$stackTrace")]
    public string DstackTrace { set { stackTrace = value; } }

    [JsonPropertyName("$payloadPath")]
    public string DpayloadPath { set { payloadPath = value; } }



    // Die Sage 100 liefert Fehlerobjekte ohne das "$"-Zeichen zurück, deshalb müssen die Werte gedoppelt werden,
    // da JsonProperty nur einzelne Werte zulässt.

    [JsonPropertyName("severity")]
    public string severity { get; set; }

    [JsonPropertyName("sdataCode")]
    public string sdataCode { get; set; }

    [JsonPropertyName("applicationCode")]
    public string applicationCode { get; set; }

    [JsonPropertyName("message")]
    public string message { get; set; }

    [JsonPropertyName("stackTrace")]
    public string stackTrace { get; set; }

    [JsonPropertyName("payloadPath")]
    public string payloadPath { get; set; }
}

