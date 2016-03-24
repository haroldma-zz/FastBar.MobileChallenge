using FastBar.MobileChallenge.Models;
using SQLite.Net;

namespace FastBar.MobileChallenge.Services
{
    public interface ILocalDataService
    {
        TableQuery<Event> Events { get; set; }

        void Delete(object obj);

        void DeleteAll<T>();

        void Insert(object obj);

        void Update(object obj);
    }
}