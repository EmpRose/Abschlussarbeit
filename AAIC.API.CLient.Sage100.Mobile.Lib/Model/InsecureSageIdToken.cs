using System;
using System.Collections.Generic;
using System.Text;

namespace AAIC.API.CLient.Sage100.Mobile.Lib.Model
{
    public class InsecureSageIdToken
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expiry { get; set; }
    }
}
