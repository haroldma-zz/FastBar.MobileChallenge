using System;
using System.Threading.Tasks;
using FastBar.MobileChallenge.Requests;
using FastBar.MobileChallenge.Responses;
using Portable.FluentRest.Http;

namespace FastBar.MobileChallenge.Services
{
    public interface IApiService
    {
        event EventHandler<AuthStateChangedEventArgs> AuthStateChanged;

        bool IsAuthenticated { get; }

        void Login(string token);

        Task<RestResponse<TokenResponse>> LoginAsync(TokenRequest request);

        void Logout();

        Task<RestResponse<TT>> SendAsync<T, TT>(T request) where T : FastBarRequestObject<TT>;
    }
}