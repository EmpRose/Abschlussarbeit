using AAIC.API.CLient.Sage100.Mobile.Lib.Token;

namespace AAIC.API.CLient.Sage100.Mobile.App.Model
{
    public class ApiSettings
    {
        public ISageIdToken Token { get; internal set; }
        public EntitlementSelectionItem Entitlement { get; internal set; }
        public DatasetSelectionItem Dataset { get; internal set; }
    }
}
