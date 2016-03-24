using SQLite.Net.Attributes;

namespace FastBar.MobileChallenge.Models
{
    public class Event
    {
        public int BarOperatorUserId { get; set; }

        public string CloudinaryPublicImageId { get; set; }

        public string DateTimeEndUtc { get; set; }

        public string DateTimeStartUtc { get; set; }

        [PrimaryKey]
        public int EventId { get; set; }

        public string EventKey { get; set; }

        public string Name { get; set; }
    }
}