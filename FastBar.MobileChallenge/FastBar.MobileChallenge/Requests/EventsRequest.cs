using FastBar.MobileChallenge.Responses;
using Portable.FluentRest.Extensions;

namespace FastBar.MobileChallenge.Requests
{
    public class EventsRequest : FastBarRequestObject<EventsResponse>
    {
        public EventsRequest() : base("api/events")
        {
            // Configure the request
            this.Get()
                .Query("userTypeFilter", "Operating");
        }
    }
}