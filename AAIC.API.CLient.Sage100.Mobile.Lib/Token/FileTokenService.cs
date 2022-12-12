using Integrative.Encryption;
using AAIC.API.CLient.Sage100.Mobile.Lib.Extensions;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AAIC.API.CLient.Sage100.Mobile.Lib.Model;

namespace AAIC.API.CLient.Sage100.Mobile.Lib.Token
{// Diese Klasse niemals beim Kunden verwenden, nur für Übungszwecke
    public class FileTokenService : ITokenService
    {
        string file = @"C:\temp\sageid.token";

        public void DeleteToken()
        {
            File.Delete(file);
        }

        public async Task<ISageIdToken> GetTokenAsync()
        {
            if (!File.Exists(file))
                return null;

            // Daten auslesen
            var protectedBase64Json = File.ReadAllText(file);
            // Bytes vom Base64-String auslesen
            var protectedJson = Convert.FromBase64String(protectedBase64Json);

            // Daten entschlüsseln
            var json = CrossProtect.Unprotect(protectedJson, null, DataProtectionScope.CurrentUser);

            // In Objekt deserialisieren
            var insecureToken = JsonSerializer.Deserialize<InsecureSageIdToken>(json);

            // Sicheres Objekt zurückgeben
            return new SageIdToken()
            {
                AccessToken = insecureToken.AccessToken.ToSecureString(),
                RefreshToken = insecureToken.RefreshToken.ToSecureString(),
                Expiry = insecureToken.Expiry
            };
        }

        public Task<bool> SaveTokenAsync(ISageIdToken token)
        {
            // Objekt für die Serialisieren erstellen
            var insecureToken = new InsecureSageIdToken()
            {
                AccessToken = token.AccessToken.FromSecureString(),
                RefreshToken = token.RefreshToken.FromSecureString(),
                Expiry = token.Expiry.ToUniversalTime(),
            };

            // Objekt serialisieren
            var json = JsonSerializer.Serialize(insecureToken);

            // JSON in verschlüsselte Bytes konvertieren
            var protectedJson = CrossProtect.Protect(Encoding.UTF8.GetBytes(json), null, DataProtectionScope.CurrentUser);

            // Verschlüsselte Daten in Base64 kodieren
            var protectedBase64Json = Convert.ToBase64String(protectedJson);

            File.WriteAllText(file, protectedBase64Json);

            return Task.FromResult(true);
        }
    }
}
