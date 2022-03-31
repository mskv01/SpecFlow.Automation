using RestSharp;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using Versatile.Specflow.ExtentReports.ApiAutomatedTesting.Helpers;
using static Versatile.Specflow.ExtentReports.ApiAutomatedTesting.Helpers.GenericRequests;

namespace Versatile.Specflow.ExtentReports.ApiAutomatedTesting.StepDefinitions
{
    [Binding]
    public class VotesInCatAPISteps
    {
        public static string ReturnDateHours() => DateTime.Now.ToString("ddhhmmss");

        public readonly string key = "ce3113e5-901d-4001-b6cc-e78a52445e43";
        public readonly string urlVotes = "https://api.thecatapi.com/v1/votes/";
        public readonly string urlVotosPorId = "https://api.thecatapi.com/v1/votes/{vote_id}";
        public readonly string bodyLoveIt = "{\"image_id\": \"vli" + ReturnDateHours() + "\", \"value\": \"true\", \"sub_id\": \"demo-4b3faa\"}";
        public readonly string bodyNopeIt = "{\"image_id\": \"npi" + ReturnDateHours() + "\", \"value\": \"false\", \"sub_id\": \"demo-4b3faa\"}";
        private readonly GenericRequests generic;
        private RestClient client;
        private RestRequest request;
        private List<Response> responseList;
        private Response response;

        public VotesInCatAPISteps() => generic = new GenericRequests();

        [Given(@"that the user makes the POST request to vote Love It on a photo")]
        public void GivenThatTheUserMakesThePOSTRequestToVoteLoveItOnAPhoto()
        {
            client = generic.GetClient(urlVotes);
            request = generic.PostRequest(bodyLoveIt, string.Empty, key);
        }
        
        [Given(@"that the user makes the POST request to vote Nope It on a photo")]
        public void GivenThatTheUserMakesThePOSTRequestToVoteNopeItOnAPhoto()
        {
            client = generic.GetClient(urlVotes);
            request = generic.PostRequest(bodyNopeIt, string.Empty, key);
        }
        
        [Given(@"that the user makes the GET request to return the list with their votes")]
        public void GivenThatTheUserMakesTheGETRequestToReturnTheListWithTheirVotes()
        {
            client = generic.GetClient(urlVotes);
            request = generic.GetRequest(string.Empty, key, string.Empty);
        }
        
        [Given(@"the user makes a GET request to return a specific vote")]
        public void GivenTheUserMakesAGETRequestToReturnASpecificVote()
        {
            client = generic.GetClient(urlVotes);
            request = generic.PostRequest(bodyNopeIt, string.Empty, key);
            response = generic.GetResponse(client, request);
            client = generic.GetClient(urlVotosPorId);
            request = generic.GetRequest(string.Empty, key, response.id);
        }
        
        [Given(@"the user makes a GET request to delete a specific vote")]
        public void GivenTheUserMakesAGETRequestToDeleteASpecificVote()
        {
            client = generic.GetClient(urlVotes);
            request = generic.PostRequest(bodyNopeIt, string.Empty, key);
            response = generic.GetResponse(client, request);
            client = generic.GetClient(urlVotosPorId);
            request = generic.DeleteRequest(string.Empty, key, response.id);
        }

        [When(@"the user receives the response of the Love It vote returned by the API")]
        public void WhenTheUserReceivesTheResponseOfTheLoveItVoteReturnedByTheAPI() => response = generic.GetResponse(client, request);

        [When(@"the user receives the response of the Nope It vote returned by the API")]
        public void WhenTheUserReceivesTheResponseOfTheNopeItVoteReturnedByTheAPI() => response = generic.GetResponse(client, request);

        [When(@"the user receives the response from the vote list returned by the API")]
        public void WhenTheUserReceivesTheResponseFromTheVoteListReturnedByTheAPI() => responseList = generic.GetListResponse(client, request);

        [When(@"the user receives the response of the specific vote returned by the API")]
        public void WhenTheUserReceivesTheResponseOfTheSpecificVoteReturnedByTheAPI() => response = generic.GetResponse(client, request);

        [When(@"the user receives the response of the specific vote deleted returned by the API")]
        public void WhenTheUserReceivesTheResponseOfTheSpecificVoteDeletedReturnedByTheAPI() => response = generic.GetResponse(client, request);

        [Then(@"the user checks the response of the Love It vote returned by the API")]
        public void ThenTheUserChecksTheResponseOfTheLoveItVoteReturnedByTheAPI() => generic.VerifyStatusCode(response);

        [Then(@"the user checks the response of the Nope It vote returned by the API")]
        public void ThenTheUserChecksTheResponseOfTheNopeItVoteReturnedByTheAPI() => generic.VerifyStatusCode(response);

        [Then(@"the user checks the vote list response returned by the API")]
        public void ThenTheUserChecksTheVoteListResponseReturnedByTheAPI() => generic.VerifyStatusCode(responseList);

        [Then(@"the user checks the response of the specific vote returned by the API")]
        public void ThenTheUserChecksTheResponseOfTheSpecificVoteReturnedByTheAPI() => generic.VerifyStatusCode(response);

        [Then(@"the user checks the response of the specific vote deleted returned by the API")]
        public void ThenTheUserChecksTheResponseOfTheSpecificVoteDeletedReturnedByTheAPI() => generic.VerifyStatusCode(response);
    }
}
