using System.Net;
using System.Net.Http;

namespace Portable.FluentRest.Http
{
    public class RestResponse
    {
        internal RestResponse(HttpResponseMessage response)
        {
            StatusCode = response.StatusCode;
            ReasonPhrase = response.ReasonPhrase;
            IsSuccessStatusCode = response.IsSuccessStatusCode;
        }

        public bool IsSuccessStatusCode { get; internal set; }

        public string ReasonPhrase { get; internal set; }

        public HttpStatusCode StatusCode { get; internal set; }
    }

    public sealed class RestResponse<T> : RestResponse
    {
        internal RestResponse(HttpResponseMessage response, T deserializedResponse) : base(response)
        {
            DeserializedResponse = deserializedResponse;
        }

        public T DeserializedResponse { get; }
    }
}