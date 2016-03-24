using FastBar.MobileChallenge.Responses;
using Portable.FluentRest.Extensions;

namespace FastBar.MobileChallenge.Requests
{
    public class TokenRequest : FastBarRequestObject<TokenResponse>
    {
        public TokenRequest(string username, string password) : base("token")
        {
            // Configure the request
            this.Post()
                .Parameter(nameof(username), username)
                .Parameter(nameof(password), password)
                .Parameter("grant_type", "password");
        }
    }
}