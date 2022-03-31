using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Versatile.Specflow.ExtentReports.AzureTestParams
{
    public class GetTestCaseParams
    {
        public DataSet Params { get; private set; }
        public string TestCaseJASON { get; private set; }
        public string VstsURI { get; set; }
        public string Pat { get; set; }

        public async Task<DataSet> GetParams(string workitemID)
        {
            try
            {
                string url = VstsURI + "/DefaultCollection/_apis/wit/workitems?ids=" + workitemID + "&api-version=1.0";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue(
                            "Basic",
                            Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", "", Pat)))
                        );
                    using (HttpResponseMessage response = client.GetAsync(url).Result)
                    {
                        DataSet dataSet = new DataSet();
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();
                        TestCaseJASON = responseBody;
                        Rootobject ro = JsonConvert.DeserializeObject<Rootobject>(responseBody);
                        DataTable dataTable = new DataTable();
                        System.IO.StringReader xmlSR = new System.IO.StringReader(ro.Value[0].Fields.MicrosoftVSTSTCMLocalDataSource.ToString());
                        dataSet.ReadXml(xmlSR, XmlReadMode.Auto);
                        Params = dataSet;
                        return dataSet;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }

        public async Task<List<string>> GetTestCasesByQuery(string workitemQuery)
        {
            try
            {
                string url = this.VstsURI + "/DefaultCollection/_apis/wit/wiql?api-version=1.0";
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", "", Pat)))
                    );
                    client.BaseAddress = new Uri(url);
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
                    request.Content = new StringContent(workitemQuery, Encoding.UTF8, "application/json");
                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    string r = await response.Content.ReadAsStringAsync();
                    JObject jr = JObject.Parse(r);
                    var i = from p in jr["workItems"] select (string)p["id"];
                    List<string> ids = i.ToList();
                    return ids;
                }
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }
    }
}