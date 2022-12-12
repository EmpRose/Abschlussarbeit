using IdentityModel.Client;

namespace AAIC.API.CLient.Sage100.Mobile.Lib.OpenId
{
    public interface ISageIdSettings // Erfragt die für eine sichere Arbeit benötigten Eigenschaften ab.
    {
        string Authority { get; }                           //Folgende Eigenschaften werden für die Loging, Logout und co Methoden benötigt
        string ClientId { get; }                    
        Parameters FrontChannelExtraParameters { get; }     // Das IdentityModel.OidcClient NuGet Paket stellt den Datentyp "Parameters" bereit
                                                            // Übernimmt die UI Properties des Users/Clients
        string RedirectUri { get; }
        string Scope { get; }

        // string Domain { get; set; }
         // string Audience { get; set; }
    }
}