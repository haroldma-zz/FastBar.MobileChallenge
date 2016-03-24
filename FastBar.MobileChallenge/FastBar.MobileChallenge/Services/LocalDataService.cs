using FastBar.MobileChallenge.Common;
using FastBar.MobileChallenge.Models;
using FastBar.MobileChallenge.Requests;
using SQLite.Net;

namespace FastBar.MobileChallenge.Services
{
    public class LocalDataService : DbContext, ILocalDataService
    {
        public LocalDataService(SQLiteConnection connection) : base(connection)
        {
        }

        public TableQuery<Event> Events { get; set; }
    }
}