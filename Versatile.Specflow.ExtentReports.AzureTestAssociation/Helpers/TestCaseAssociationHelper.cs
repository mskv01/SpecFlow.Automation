using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using System;
using System.Collections.Generic;
using Versatile.Specflow.ExtentReports.CryptoEngine.CryptoEngine;

namespace Versatile.Specflow.ExtentReports.AzureTestAssociation.Helpers
{
    public class TestCaseAssociationHelper
    {
        private static readonly string _vstsPat = "/VcGGZ+g3OX4nqKQAM32byqU0ha0yjZ5go2zA3bLhvX/WYME4kx+qqL0SKGVbiIg0G+LNXBZzliJAPlXkYOgJg==";
        private static readonly string _cryptoKey = "Cybernetics CRM 4.0 Word Addin";
        private static readonly string _vstsUrl = "https://fabiotestproject.visualstudio.com/";
        public static WorkItemTrackingHttpClient WitClient;

        public static void TestCaseAssociate(int testCaseId, string automatedTestStorage, string automatedTestName)
        {
            try
            {
                WitClient = new VssConnection(new Uri(_vstsUrl), new PatCredentials(string.Empty, Crypto.Decrypt(_vstsPat, _cryptoKey))).GetClient<WorkItemTrackingHttpClient>();
                Dictionary<string, object> fields = new Dictionary<string, object>
                {
                    { "Microsoft.VSTS.TCM.AutomatedTestStorage", automatedTestStorage },
                    { "Microsoft.VSTS.TCM.AutomatedTestType", "Unit Test" },
                    { "Microsoft.VSTS.TCM.AutomationStatus", "Automated" }, // VIEW THE STATUS ON YOUR TFS/AZURE!
                    { "Microsoft.VSTS.TCM.AutomatedTestName", automatedTestName }
                };
                UpdateWorkItem(testCaseId, fields);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                if (ex.InnerException != null) 
                    Console.WriteLine("Detailed Info: " + ex.InnerException.Message);
                Console.WriteLine("Stack:\n" + ex.StackTrace);
            }
        }

        public static void ClearTestCase(int testCaseId)
        {
            try
            {
                WitClient = new VssConnection(new Uri(_vstsUrl), new PatCredentials(string.Empty, Crypto.Decrypt(_vstsPat, _cryptoKey))).GetClient<WorkItemTrackingHttpClient>();
                Dictionary<string, object> fields = new Dictionary<string, object>
                {
                    { "Microsoft.VSTS.TCM.AutomatedTestStorage", "" },
                    { "Microsoft.VSTS.TCM.AutomatedTestType", "" },
                    { "Microsoft.VSTS.TCM.AutomationStatus", "Not Automated" }, // VIEW THE STATUS ON YOUR TFS/AZURE!
                    { "Microsoft.VSTS.TCM.AutomatedTestName", "" }
                };
                UpdateWorkItem(testCaseId, fields);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                if (ex.InnerException != null)
                    Console.WriteLine("Detailed Info: " + ex.InnerException.Message);
                Console.WriteLine("Stack:\n" + ex.StackTrace);
            }
        }

        public static WorkItem UpdateWorkItem(int WIId, Dictionary<string, object> Fields)
        {
            JsonPatchDocument patchDocument = new JsonPatchDocument();

            Fields.Keys.ForEach(Key => patchDocument.Add(new JsonPatchOperation() 
            {
                Operation = Operation.Add,
                Path = "/fields/" + Key,
                Value = Fields[Key]
            }));
            return WitClient.UpdateWorkItemAsync(patchDocument, WIId).Result;
        }

    }
}
