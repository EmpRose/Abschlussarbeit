using System;
using System.Security;

namespace AAIC.API.CLient.Sage100.Mobile.Lib.Token
{
    public interface ISageIdToken
    {
        SecureString AccessToken { get; set; }
        DateTime Expiry { get; set; }
        SecureString RefreshToken { get; set; }
    }
}