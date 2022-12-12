using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

    public class Entitlements // Umbenennen des RootObjekts in Entitlements
    {
        public int totalentitlements { get; set; }
        [JsonPropertyName("$resources")]            //JsonPropertyName hinzufügen , dieses mal nur bei der Entitlement Property
        public Entitlement[] resources { get; set; } // Umbenennen des Resources in Entitlement
    }

