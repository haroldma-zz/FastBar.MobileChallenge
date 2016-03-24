using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Portable.FluentRest.Http
{
    public class QueueFakeResponseHandler : DelegatingHandler
    {
        private readonly Queue<HttpResponseMessage> _queuedResponses;

        public QueueFakeResponseHandler()
        {
            _queuedResponses = new Queue<HttpResponseMessage>();
            Sent = new List<Tuple<HttpRequestMessage, string>>();
        }

        public List<Tuple<HttpRequestMessage, string>> Sent { get; }

        public void Enqueue(HttpResponseMessage message)
        {
            _queuedResponses.Enqueue(message);
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            string content = null;
            if (request.Content != null)
            {
                content = await request.Content.ReadAsStringAsync();
            }
            Sent.Add(Tuple.Create(request, content));
            var response = _queuedResponses.Dequeue()
                ?? new HttpResponseMessage(HttpStatusCode.NotFound) { RequestMessage = request };

            return response;
        }
    }
}