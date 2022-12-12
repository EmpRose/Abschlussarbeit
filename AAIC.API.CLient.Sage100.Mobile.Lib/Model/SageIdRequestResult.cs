using AAIC.API.CLient.Sage100.Mobile.Lib.Token;
using System;
using System.Collections.Generic;
using System.Text;

namespace AAIC.API.CLient.Sage100.Mobile.Lib.Model
{
    public class SageIdRequestResult                // Mit dieser Klasse bringe ich in Erfahrung ob die Anfrage mit einem SageIdToken zur Anmeldung erfolgreich war
                                                    // und wenn nicht, woran es scheiterte.
    {
        public bool IsError { get; set; }           // Belege - Erhalte Inhalt der boolischen Variable "IsError"
        public string Error { get; set; }           // Variable für den Inhalt einer Fehlerausgabe
        public ISageIdToken Token { get; set; }     // Objekt der Klasse SageIdToken zum Auslesen und Schreiben.
    }
}
