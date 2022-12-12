using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;


    public class DiagnosList
    {
        // Für jede Property muss das jeweilige JSON-Attribut "JsonPropertyName" hinzugefügt werden [JsonPropertyName("$NameVomProperty")]
        [JsonPropertyName("$diagnoses")]
        public Diagnoses[] diagnoses { get; set; }
    }

