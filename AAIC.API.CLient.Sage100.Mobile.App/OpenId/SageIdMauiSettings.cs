using AAIC.API.CLient.Sage100.Mobile.Lib.OpenId;
using IdentityModel.Client;


namespace AAIC.API.CLient.Sage100.Mobile.App.OpenId
{
    public class SageIdMauiSettings : ISageIdSettings
    {
        public string Authority => "https://id.sage.com/" ;
        //public string Authority => "https://PC-10:5493/";

        public string ClientId => "Qg5GjXiG8ojopNXu3oBM4rJS96SMIqzp";

        public Parameters FrontChannelExtraParameters => new Parameters(new List<KeyValuePair<string, string>> { new KeyValuePair<string,string>("audience", "s100de/sage100") });

        public string RedirectUri => "myprot://myapp.com/callback";

        public string Scope => "openid token access_token offline:access email profile";
    }
}
