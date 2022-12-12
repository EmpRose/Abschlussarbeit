using AAIC.API.CLient.Sage100.Mobile.Lib.Extensions;
using AAIC.API.CLient.Sage100.Mobile.Lib.Model;
using Integrative.Encryption;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AAIC.API.CLient.Sage100.Mobile.Lib.Token
{
    public class TokenService : ITokenService
    {
        private readonly string tokenFile = "sageID.token";                                        // deklariert private "nur-lesen" Zeichenkette mit Inhalt "sageID.token"
        private readonly IsolatedStorageScope scope;                                               // deklariert private "nur-lesen" vom Gültigkeitsbereich mit dem Namen "scope"

        public TokenService()
        {
            scope = IsolatedStorageScope.User |                                                    // scope erhält den Gültigkeitsbereich "1" User oder "2" Domain oder "4" Assembly 
                    IsolatedStorageScope.Domain |
                    IsolatedStorageScope.Assembly;
        }

        // Der TokenService bietet drei Methoden an Token speichern, Token löschen und Token erhalten

        public async Task<bool> SaveTokenAsync(ISageIdToken token)                                 // "async" Folgeanweisungen werden auch begonnen,
                                                                                                   // wenn die Vorherige nicht abgeschlossen wurde.
        {
            using (var isoStore = IsolatedStorageFile.GetStore(scope, null,null))                 // using - anwenden zusätzlicher Bibliotheken oder Methoden
                                                                                                   // oder Engine und verwirft diese nach der Benutzung
                                                                                                   // in diesem Fall das IsolatedStorageFile.GetScope (erhalte Umfang)
            {
                // Objekt für die Serialisierung erstellen
                var insecureToken = new InsecureSageIdToken()                                      // erstellt ein Objekt der Klasse InsecureSageIdToken und übergibt Werte aus der "Sicheren Zeichenkette"
                {
                    AccessToken = token.AccessToken.FromSecureString(),
                    RefreshToken = token.RefreshToken.FromSecureString(),
                    Expiry = token.Expiry.ToUniversalTime()
                };

                // Objekt serialisieren
                var json = JsonSerializer.Serialize(insecureToken);                                 // Objekt insecureToken wird in Json Form serialisiert
                                                                                                    // und in die Variable "json" übergeben

                //JSON in verschlüsselte Bytes konvertieren                                         // Mithilfe von CrossProtect-Protect
                var protectedJson = CrossProtect.Protect(
                    Encoding.UTF8.GetBytes(json), null, DataProtectionScope.CurrentUser);           // Codierung der UTF8 Zeichen aus Variable "json"
                                                                                                    // und den Datenschutzbereich "Aktueller Benutzer"
                                                                                                    // in Variable "protectedJson

                // Verschlüsselte Daten in Base64 kodieren
                var protectedBase64Json = Convert.ToBase64String(protectedJson);                    // Verschlüsselt die Codierte Variable "protectedJson"
                                                                                                    // in Variable "protectedBase64Json" auf Base64 Codierung

                //Base-64-Kodierten String abspeichern
                using (var isoStream = new IsolatedStorageFileStream(
                    tokenFile, FileMode.Create, isoStore))                                          // Schreibt Daten in eine isolierte Storage-Datei
                                                                                                    // und gibt den Inhalt, die Art und den Ort an mit Übergabe an "isoStream"
                {
                    using (var writer = new StreamWriter(isoStream))                                // Schreibt die Daten aus dem isoliertem Lager in die Variable "writer"                                                         
                    {
                        await writer.WriteAsync(protectedBase64Json);                               // Warte bis eine Bytesequenz in den Stream "writer" geschrieben.
                                                                                                    // Da die Variable "isoStore" verschiedene Inhalte enthält, werden diese gleichzeitig (asynchron) geschrieben
                    }
                }
                return true;
            }
        }
        public void DeleteToken()
        {
            using (var isoStore = IsolatedStorageFile.GetStore(scope, null, null))                  // Übergibt ein Objekt Inhalt aus dem Isoliertem "Daten Lager" in "isoStorage"           
            {
                isoStore.DeleteFile(tokenFile);                                                     // Löscht den Inhalt vom "tokenFile" aus dem Isoliertem "Daten Lager"
            }
        }

        public async Task<ISageIdToken> GetTokenAsync()                                             // Asynchrone Ausführung "Token Erhalten"
                                                                                                    // (nur nach dem SaveTokenAsync da sonst leer)
        {
            using (var isoStore = IsolatedStorageFile.GetStore(scope, null, null))                  // Übergibt Objekt aus dem Isoliertem Speicher an isoStore
            {
                if (isoStore.FileExists(tokenFile))                                                 // Wenn "isoStore" - "tokenFile" enthält
                {
                    using (var isoStream = new IsolatedStorageFileStream(                           // Schreibt Objektdaten aus dem Isoliertem Speicher "tokenFile" - "öffnen" - "aus isoStore"
                        tokenFile, FileMode.Open, isoStore))
                    {
                        using (StreamReader reader = new StreamReader(isoStream))                   // Liest die Daten aus dem isoStream (die es von isoStore erhalten hat)
                        {
                            //Daten auslesen
                            var protectedBase64Json = await reader.ReadToEndAsync();                // Liest asynchron alle Daten aus "reader" in "protectedBase64Json" ein

                            //Bytes vom 64-Base-kodierter Zeichenkette auslesen
                            var protectedJson = Convert.FromBase64String(protectedBase64Json);      // Convertiert erhaltene Daten aus dem 64Byte Code in "protectedJson"

                            //Daten entschlüsseln
                            var json = CrossProtect.Unprotect(protectedJson, null,                  // mit Cross Protect entschlüsseln der einzelnen Fragmente die aus dem 64Byte Code
                                                                                                    // convertiert wurden mit dem Datenschutzbereich des "Aktueller Benutzer"´s
                                DataProtectionScope.CurrentUser);

                            // In Objekt deserialisieren
                            var insecureToken = JsonSerializer.Deserialize<InsecureSageIdToken>(json); // Deserialisiert als InsecureSageIdToken aus "json" in "insecureToken"

                            //Sicheres Objekt zurückgeben
                            return new SageIdToken()                                                // Ordnet erhaltenen Inhalt aus insecureToken auf ein SageIdToken
                                                                                                    // und gibt die Werte zurück als SageIdToken und sichert nach "ToSecureString" Methode ab
                            {
                                AccessToken = insecureToken.AccessToken.ToSecureString(),
                                RefreshToken = insecureToken.RefreshToken.ToSecureString(),
                                Expiry = insecureToken.Expiry
                            };
                        }
                    }
                }
                return null;
            }
        }
    }
}
