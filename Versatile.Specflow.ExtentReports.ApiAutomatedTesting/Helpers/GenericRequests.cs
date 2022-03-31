using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;

namespace Versatile.Specflow.ExtentReports.ApiAutomatedTesting.Helpers
{
    public class GenericRequests
    {
        public RestClient GetClient(string url) => new RestClient(url);

        public RestRequest PostRequest(string body, string path = null, string key = null)
        {
            var request = new RestRequest(path, Method.POST);
            request.AddHeader("x-api-key", key).AddParameter("application/json", body, ParameterType.RequestBody);
            return request;
        }

        public RestRequest GetRequest(string path = null, string key = null, string id = null)
        {
            var request = new RestRequest(path, Method.GET);
            request.AddHeader("x-api-key", key).AddUrlSegment("vote_id", id);
            return request;
        }

        public RestRequest DeleteRequest(string path = null, string key = null, string id = null)
        {
            var request = new RestRequest(path, Method.DELETE);
            request.AddHeader("x-api-key", key).AddUrlSegment("vote_id", id);
            return request;
        }

        public Response GetResponse(RestClient client, RestRequest request)
        {
            var response = client.Execute(request).Content;
            return JsonConvert.DeserializeObject<Response>(response);
        }

        public List<Response> GetListResponse(RestClient client, RestRequest request)
        {
            var response = client.Execute(request).Content;
            return JsonConvert.DeserializeObject<List<Response>>(response);
        }

        public void VerifyStatusCode(Response response, string fileName = null)
        {
            Hooks._response = string.Empty;
            if (string.IsNullOrEmpty(response.message))
            {
                Hooks._response +=
                "</br>ID: " + response.id + "</br>" +
                "country_code: " + response.country_code + "</br>" +
                "created_at: " + response.created_at + "</br>" +
                "image_id: " + response.image_id + "</br>" +
                "sub_id: " + response.sub_id + "</br>" +
                "user_id: " + response.user_id + "</br>" +
                "value: " + response.value + "</br>";
            }
            else
            {
                if (response.message.Contains("SUCCESS"))
                    Hooks._response = response.message; //FileHelper.CreateFile("result" + fileName + ".txt", message); // When you want to put the message in a txt file and link to the report             
                else
                    Assert.Fail(response.message);
            }                
        }

        public void VerifyStatusCode(List<Response> response, string fileName = null)
        {
            Hooks._response = string.Empty;
            response.ForEach(resp =>
                Hooks._response += 
                "</br>ID: " + resp.id + "</br>" +
                "country_code: " + resp.country_code + "</br>" +
                "created_at: " + resp.created_at + "</br>" +
                "image_id: " + resp.image_id + "</br>" +
                "sub_id: " + resp.sub_id + "</br>" +
                "value: " + resp.value + "</br>"
            );
            //FileHelper.CreateFile("result" + fileName + ".txt", Hooks._response); // When you want to put the message in a txt file and link to the report
        }


        public class Response
        {
            public string message;
            public string status;
            public string level;
            public string id;
            public string user_id;
            public string image_id;
            public string sub_id;
            public string created_at;
            public string value;
            public string country_code;
        }

    }
}
