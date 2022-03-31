using RestSharp;
using TechTalk.SpecFlow;
using Versatile.Specflow.ExtentReports.ApiAutomatedTesting.Helpers;
using static Versatile.Specflow.ExtentReports.ApiAutomatedTesting.Helpers.GenericRequests;

namespace Versatile.Specflow.ExtentReports.ApiAutomatedTesting.StepDefinitions
{
    [Binding]
    public class RegisterUserInCatAPISteps
    {
        public readonly string _body = "{\"email\": \"fabioalves77777@gmail.com\", \"appDescription\": \"teste the cat api\"}";
        public readonly string _url = "https://api.thecatapi.com";
        public readonly string _path = "/v1/user/passwordlesssignup";
        private readonly GenericRequests generic;
        private RestClient client;
        private RestRequest request;
        private Response response;

        public RegisterUserInCatAPISteps() => generic = new GenericRequests();

        [Given(@"that the user makes the POST request to generate an API Key")]
        public void GivenThatTheUserMakesThePOSTRequestToGenerateAnAPIKey()
        {
            client = generic.GetClient(_url);
            request = generic.PostRequest(_body, _path);
        }

        [When(@"the user receives the API response")]
        public void WhenTheUserReceivesTheAPIResponse() => response = generic.GetResponse(client, request);

        [Then(@"the user verify the API response")]
        public void ThenTheUserVerifyTheAPIResponse() => generic.VerifyStatusCode(response);
    }
}
