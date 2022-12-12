using AAIC.API.CLient.Sage100.Mobile.Lib.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;

namespace AAIC.API.CLient.Sage100.Mobile.Lib.Exceptions
{
    public class HttpException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }
        public DiagnosList DiagnosList { get; private set; } // PRIVATE SET LESEN

        public HttpException (HttpStatusCode statusCode, string content) : base (content) // BASE CONTENT LESEN
        {
            StatusCode = statusCode;
            if(string.IsNullOrWhiteSpace(content) || statusCode == HttpStatusCode.Unauthorized)
            {
                //Sonderfälle wie beispielsweise Statuscode 401
                var message = statusCode.ToString();
                if (!string.IsNullOrWhiteSpace(content))                // Wenn "content" nicht leer oder mit Leerzeichen gefüllt ist
                    message = content;                                  // Erstellt Variable "message" und übergibt Inhalt aus "content"

                DiagnosList = new DiagnosList();                        // Erstellt das Objekt "DiagnosList" anhand der Klasse "DiagnosList"
                DiagnosList.diagnoses = new Diagnoses[1];               // Inhalt der Diagnosen aus der JSON Diagnoses Liste werden im Index "1" abgelegt
                DiagnosList.diagnoses[0] = new Diagnoses()              // Index "0" erhält eine neue Liste der Diagnoses JSON Liste mit dem Feld "Nachricht"
                {
                    message = "Fehler: " + message                      // Das Feld "Nachricht" baut eine Zeichenkette mit "Fehler: " + Inhalt der Nachricht 
                };
            }
            else
                DiagnosList = JsonSerializer.Deserialize<DiagnosList>(content); // Im anderen Fall erhält DiagnosList den "entschlüsselten"/"deserealisierten" Inhalt aus "content"
        }
    }
}
