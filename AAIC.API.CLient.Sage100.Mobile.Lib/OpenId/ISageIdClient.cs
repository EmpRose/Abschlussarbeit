using AAIC.API.CLient.Sage100.Mobile.Lib.Model;
using AAIC.API.CLient.Sage100.Mobile.Lib.Token;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AAIC.API.CLient.Sage100.Mobile.Lib.OpenId
{
    public interface ISageIdClient // Interface der Klasse SageIdClient
    {
        Task<DatasetResponse> GetDatasetsAsync(ISageIdToken sageIdToken, int entitlementid);
        Task<IEnumerable<Entitlement>> GetEntitlementAsync(ISageIdToken sageIdToken);
        Task<SageIdRequestResult> LoginAsync();
        Task<bool> LogoutAsync();
        Task<SageIdRequestResult> RefreshAccessTokenAsync(ISageIdToken sageIdToken);
    }
}