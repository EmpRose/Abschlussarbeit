using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace AAIC.API.CLient.Sage100.Mobile.Lib.Token
{
    public class SageIdToken : ISageIdToken
    {
        public SecureString AccessToken { get; set; }       // SecureString, verwirft den Inhalt im Speicher, wenn dieser nicht mehr gebraucht wird.
        public SecureString RefreshToken { get; set; }
        public DateTime Expiry { get; set; }

        //public string Audience { get; set; }
    }
}
