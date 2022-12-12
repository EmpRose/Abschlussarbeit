using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;


    public class DatasetResponse
    {
        [JsonPropertyName("$url")]
        public string url { get; set; }

        [JsonPropertyName("$descriptor")]
        public string descriptor { get; set; }

        [JsonPropertyName("$updated")]
        public DateTime updated { get; set; }

        [JsonPropertyName("$resources")]
        public DatasetItem[] resources { get; set; }
    }

