using FastBar.MobileChallenge.Requests;
using FastBar.MobileChallenge.Services;
using FastBar.MobileChallenge.Views;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;

namespace FastBar.MobileChallenge.ViewModels
{
    public class LoginViewModel : NavigationAwareViewModel
    {
        private readonly IApiService _apiService;
        private string _errorMessage;
        private string _password;
        private string _username;

        public LoginViewModel(IApiService apiService)
        {
            _apiService = apiService;
            LoginCommand = new Command(LoginExecute);
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

        public Command LoginCommand { get; }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                SetProperty(ref _password, value);
            }
        }

        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                SetProperty(ref _username, value);
            }
        }

        private async void LoginExecute()
        {
            ErrorMessage = null;
            if (string.IsNullOrEmpty(Username))
            {
                ErrorMessage = "Please enter your username.";
                return;
            }
            if (string.IsNullOrEmpty(Password))
            {
                ErrorMessage = "Please enter your password.";
                return;
            }

            IsBusy = true;
            var response = await _apiService.LoginAsync(new TokenRequest(Username, Password));
            IsBusy = false;
            if (response.IsSuccessStatusCode)
            {
                await Navigation.PushAsync<EventsViewModel>();
                await Navigation.RemoveAsync(this);
            }
            else
            {
                ErrorMessage = "Problem authenticating.";
            }
        }
    }
}