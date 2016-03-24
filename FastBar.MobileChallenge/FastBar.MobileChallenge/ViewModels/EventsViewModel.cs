using System.Collections.Generic;
using System.Linq;
using FastBar.MobileChallenge.Models;
using FastBar.MobileChallenge.Requests;
using FastBar.MobileChallenge.Responses;
using FastBar.MobileChallenge.Services;
using XLabs.Forms.Mvvm;

namespace FastBar.MobileChallenge.ViewModels
{
    public class EventsViewModel : NavigationAwareViewModel
    {
        private readonly IApiService _apiService;
        private readonly ILocalDataService _localDataService;
        private string _errorMessage;
        private List<Event> _events;

        public EventsViewModel(IApiService apiService, ILocalDataService localDataService)
        {
            _apiService = apiService;
            _localDataService = localDataService;

            _apiService.AuthStateChanged += ApiServiceOnAuthStateChanged;
        }

        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                SetProperty(ref _errorMessage, value);
            }
        }

        public List<Event> Events
        {
            get
            {
                return _events;
            }
            set
            {
                SetProperty(ref _events, value);
            }
        }

        public override async void OnViewAppearing()
        {
            // Display offline data
            Events = _localDataService.Events.ToList();

            IsBusy = true;
            var restResponse = await _apiService.SendAsync<EventsRequest, EventsResponse>(new EventsRequest());
            IsBusy = false;

            if (restResponse.IsSuccessStatusCode)
            {
                // store it
                foreach (var eventItem in restResponse.DeserializedResponse)
                {
                    if (Events.All(p => p.EventId != eventItem.EventId))
                    {
                        _localDataService.Insert(eventItem);
                    }
                }

                Events = _localDataService.Events.ToList();
            }
            else
            {
                ErrorMessage = $"Problem {(Events.Count == 0 ? "downloading" : "refreshing")} events.";
            }
        }

        private async void ApiServiceOnAuthStateChanged(
            object sender,
            AuthStateChangedEventArgs authStateChangedEventArgs)
        {
            if (authStateChangedEventArgs.IsAuthenticated)
            {
                return;
            }
            _localDataService.DeleteAll<Event>();
            await Navigation.PushAsync<LoginViewModel>();
            await Navigation.RemoveAsync(this);
        }
    }
}