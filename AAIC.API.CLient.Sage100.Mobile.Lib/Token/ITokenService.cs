using System.Threading.Tasks;

namespace AAIC.API.CLient.Sage100.Mobile.Lib.Token
{
    public interface ITokenService // Interface der Klasse TokenService
    {
        void DeleteToken();
        Task<ISageIdToken> GetTokenAsync();
        Task<bool> SaveTokenAsync(ISageIdToken token);
    }
}


/*
Ab Schritt 4 wird mit der Klasse "FileTokenService" gearbeitet, die erstellt wird, 
da Microsoft für die Windows Applikation einen Bug hat, der bislang nicht behoben wurden
*/