using System;

using System.Text.Json.Serialization;


    public class DatasetItem
    {
        [JsonPropertyName("$url")]
        public string url { get; set; }

        [JsonPropertyName("$key")]
        public string key { get; set; }

        [JsonPropertyName("$descriptor")]
        public string descriptor { get; set; }

        [JsonPropertyName("$updated")]
        public DateTime updated { get; set; }
    }

