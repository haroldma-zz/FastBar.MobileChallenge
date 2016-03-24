using Portable.FluentRest.Extensions;
using Portable.FluentRest.Http;

namespace FastBar.MobileChallenge.Requests
{
    public class FastBarRequestObject<T> : RestRequestObject<T>
    {
        private const string BasePath = "https://fastbar-test.azurewebsites.net/";

        public FastBarRequestObject(string path)
        {
            this.Url(BasePath + path);
        }
    }
}