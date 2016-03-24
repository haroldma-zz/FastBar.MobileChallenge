using System;
using System.Net;
using System.Threading.Tasks;
using FastBar.MobileChallenge.Requests;
using FastBar.MobileChallenge.Responses;
using FastBar.MobileChallenge.Utilities;
using Portable.FluentRest.Extensions;
using Portable.FluentRest.Http;

namespace FastBar.MobileChallenge.Services
{
    /// <summary>
    ///     Provides data for the AuthStateChanged event.
    /// </summary>
    public class AuthStateChangedEventArgs : EventArgs
    {
        public bool IsAuthenticated { get; set; }
    }

    /// <summary>
    ///     Helps with keeping track of the user session (token).
    ///     Recommended to keep a single instance if listening to the AuthStateChanged event.
    /// </summary>
    public class ApiService : IApiService
    {
        public const string CredentialResource = "FastBar";
        private readonly ICredentialUtility _credentialUtility;
        private string _accessToken;

        public ApiService(ICredentialUtility credentialUtility)
        {
            _credentialUtility = credentialUtility;

            var creds = _credentialUtility.GetCredentials(CredentialResource);
            if (creds != null)
            {
                // If credential exists use the password property to obtain the access token
                _accessToken = creds.Password;
            }
        }

        public event EventHandler<AuthStateChangedEventArgs> AuthStateChanged;

        public bool IsAuthenticated => _accessToken != null;

        public void Login(string token)
        {
            // Set the new access token
            _accessToken = token;
            // Save it for later use
            _credentialUtility.SaveCredentials(CredentialResource, CredentialResource, token);

            OnAuthStateChanged(true);
        }

        public async Task<RestResponse<TokenResponse>> LoginAsync(TokenRequest request)
        {
            if (IsAuthenticated)
            {
                Logout();
            }

            // Get the response
            var response = await request.ToResponseAsync();

            if (response.IsSuccessStatusCode)
            {
                // If the status is success, continue to store the token
                Login(response.DeserializedResponse.AccessToken);
            }

            // return the response as normal
            return response;
        }

        public void Logout()
        {
            // Set the token to null
            _accessToken = null;

            //Delete it from credentials storage
            _credentialUtility.DeleteCredentials(CredentialResource);

            OnAuthStateChanged(false);
        }

        public async Task<RestResponse<TT>> SendAsync<T, TT>(T request) where T : FastBarRequestObject<TT>
        {
            // Add/remove authorization header depending if the user is authenticated
            if (IsAuthenticated)
            {
                request.Header("Authorization", "Bearer " + _accessToken);
            }
            else if (request.Headers.ContainsKey("Authorization"))
            {
                request.Headers.Remove("Authorization");
            }

            // Let's get the response as normal
            var response = await request.ToResponseAsync();

            // If the status code is Unauthorized it means the token expired
            if (response.StatusCode == HttpStatusCode.Unauthorized && IsAuthenticated)
            {
                // No refresh tokens in this implementation, so logout
                Logout();
            }

            return response;
        }

        protected virtual void OnAuthStateChanged(bool e)
        {
            AuthStateChanged?.Invoke(this, new AuthStateChangedEventArgs { IsAuthenticated = e });
        }
    }
}