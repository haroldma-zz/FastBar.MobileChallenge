using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FastBar.MobileChallenge.Requests;
using FastBar.MobileChallenge.Responses;
using FastBar.MobileChallenge.Services;
using FastBar.MobileChallenge.Test.Responses;
using FastBar.MobileChallenge.Utilities;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Portable.FluentRest.Extensions;

namespace FastBar.MobileChallenge.Test
{
    [TestFixture]
    public class ApiServiceTest
    {
        [SetUp]
        public void SetUp()
        {
            var mock = new Mock<ICredentialUtility>();
            mock.Setup(foo => foo.GetCredentials(ApiService.CredentialResource))
                .Returns(new AppCredential("user", "token"));

            _sut = new ApiService(mock.Object);
        }

        private ApiService _sut;

        private HttpResponseMessage CreateResponseJson(string message, HttpStatusCode status = HttpStatusCode.OK)
        {
            var response = new HttpResponseMessage(status)
            {
                Content = new StringContent(message, Encoding.UTF8, "application/json")
            };

            return response;
        }

        [Test]
        public void ShouldLoadCredentials()
        {
            _sut.IsAuthenticated.Should().BeTrue();
        }

        [Test]
        public async Task ShouldLogin_AuthStateChangedEvent()
        {
            // Create a token request with a fake response
            var request = new TokenRequest("johny", "doe").Fake(CreateResponseJson(ApiResponses.Token));

            // Start monitoring auth state event
            AuthStateChangedEventArgs args = null;
            _sut.AuthStateChanged += (sender, eventArgs) => args = eventArgs;

            // Login using the api service and the fake handler
            await _sut.LoginAsync(request);

            args.Should().NotBeNull().And.BeOfType<AuthStateChangedEventArgs>()
                .Which.IsAuthenticated.Should().BeTrue();
        }

        [Test]
        public async Task ShouldLogin_IsAuthenticated()
        {
            // Create a token request with a fake response
            var request = new TokenRequest("johny", "doe").Fake(CreateResponseJson(ApiResponses.Token));

            // Login using the api service and the fake handler
            await _sut.LoginAsync(request);

            _sut.IsAuthenticated.Should().BeTrue();
        }

        [Test]
        public async Task ShouldLogin_IsNotAuthenticated()
        {
            // Create a token request with a fake response
            var request =
                new TokenRequest("johny", "doe").Fake(CreateResponseJson(ApiResponses.Token, HttpStatusCode.BadRequest));

            // Login using the api service and the fake handler
            await _sut.LoginAsync(request);

            _sut.IsAuthenticated.Should().BeFalse();
        }

        [Test]
        public void ShouldLogout_AuthStateChangedEvent()
        {
            // Start monitoring auth state event
            AuthStateChangedEventArgs args = null;
            _sut.AuthStateChanged += (sender, eventArgs) => args = eventArgs;

            _sut.Logout();

            args.Should().NotBeNull().And.BeOfType<AuthStateChangedEventArgs>()
                .Which.IsAuthenticated.Should().BeFalse();
        }

        [Test]
        public void ShouldLogout_IsNotAuthenticated()
        {
            _sut.Logout();
            _sut.IsAuthenticated.Should().BeFalse();
        }

        [Test]
        public async Task ShouldReturnEvents()
        {
            // Create an events request with a fake response
            var request = new EventsRequest().Fake(CreateResponseJson(ApiResponses.Events));

            // Send the request using the api service and the fake handler
            var restResponse = await _sut.SendAsync<EventsRequest, EventsResponse>(request);

            restResponse.DeserializedResponse.Should().NotBeEmpty();
        }
    }
}