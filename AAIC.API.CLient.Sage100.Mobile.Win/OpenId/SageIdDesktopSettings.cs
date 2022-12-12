using AAIC.API.CLient.Sage100.Mobile.Lib.OpenId;
using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenId
{
    internal class SageIdDesktopSettings : ISageIdSettings
    {
        public string Authority => "https://id.sage.com";

        public string ClientId => "Qg5GjXiG8ojopNXu3oBM4rJS96SMIqzp";

        public Parameters FrontChannelExtraParameters =>  new Parameters(new List<KeyValuePair<string,string>> {new KeyValuePair<string,string>("audience","s100de/sage100") });

        public string RedirectUri => "https://id.sage.com/mobile";

        public string Scope => "openid token access_token offline_access email profile";
    }
}
