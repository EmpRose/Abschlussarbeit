using AAIC.API.CLient.Sage100.Mobile.Lib.Token;
using AAIC.API.CLient.Sage100.Mobile.Lib.Extensions;

namespace AAIC.API.CLient.Sage100.Mobile.Lib.Repository.Tests
{
    public class TestTokenService : ITokenService
    {
        public void DeleteToken()
        {
            throw new NotImplementedException();
        }

        public Task<ISageIdToken> GetTokenAsync()
        {
            var token = new SageIdToken()
            {
                AccessToken = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6Ik56TXdPVVJHTVVZNU5ERXpRelJHUVVNMVF6azJSa1U1UVRJMU0wRTROemhGUmpWQ04wSTNOQSJ9.eyJpc3MiOiJodHRwczovL2lkLnNhZ2UuY29tLyIsInN1YiI6ImF1dGgwfGNiNDI3Y2Q0MWJjOWEyOTY5ZTk2MzhjZmNlNGM1MzdlYTE4OWUxMTMwNGRmNzcxYyIsImF1ZCI6WyJzMTAwZGUvc2FnZTEwMCJdLCJpYXQiOjE2NjYyNTE2MDcsImV4cCI6MTY2NjI4MDQwNywiYXpwIjoibllTb1VWRExwQk9qdHN0Q2Exb3NocndNMEpXaVFKdUwiLCJzY29wZSI6Im9wZW5pZCBwcm9maWxlIGVtYWlsIG9mZmxpbmVfYWNjZXNzIn0.tOSHo5mDwiUoGeOo41icqPtVuD4kNuRjLUwdi65tyR_kDLgTt0mGjg85GRZi6Yfsm6QWSWbFLfvX8gRhn2HcHcoHyEpY8J4DVNmHzbIkVLU0golIG3yTvp6V0KpmBn2B4Dna5wMyR1cUAYqaLItP_l-Iq_9yBrJI1ALR5am-I3VCnkaY7X5Nb1_tf2gVOs2CYO1lqRaU4WLYvLWrFZKhg1AnrbfwsmpUyas9w0irL1m5nHcTy1iTijwRD7zxu71teBAgeIeCfnN9g_vM8G6yJ7qQ96rL45GKbOU-P6UpFz5IuQhaxbdnXeBorITFXQlfYT1GbMhplwndbfvqd4gTpw".ToSecureString(),
                Expiry = DateTime.Now,
                RefreshToken = "".ToSecureString()
            };
            return Task.FromResult(token as ISageIdToken);
        }

        public Task<bool> SaveTokenAsync(ISageIdToken token)
        {
            throw new NotImplementedException();
        }
    }
}