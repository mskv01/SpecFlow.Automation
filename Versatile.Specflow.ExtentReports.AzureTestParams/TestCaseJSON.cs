using Newtonsoft.Json;
using System;

namespace Versatile.Specflow.ExtentReports.AzureTestParams
{
    internal class TestCaseJSON { }

    public class Rootobject
    {
        public int Count { get; set; }
        public Value[] Value { get; set; }
    }

    public class Value
    {
        public int Id { get; set; }
        public int Rev { get; set; }
        public Fields Fields { get; set; }
        public string Url { get; set; }
    }

    public class Fields
    {
        public string SystemAreaPath { get; set; }
        public string SystemTeamProject { get; set; }
        public string SystemIterationPath { get; set; }
        public string SystemWorkItemType { get; set; }
        public string SystemState { get; set; }
        public string SystemReason { get; set; }
        public string SystemAssignedTo { get; set; }
        public DateTime SystemCreatedDate { get; set; }
        public string SystemCreatedBy { get; set; }
        public DateTime SystemChangedDate { get; set; }
        public string SystemChangedBy { get; set; }
        public string SystemTitle { get; set; }
        public int MicrosoftVSTSCommonPriority { get; set; }
        public DateTime MicrosoftVSTSCommonStateChangeDate { get; set; }
        public DateTime MicrosoftVSTSCommonActivatedDate { get; set; }
        public string MicrosoftVSTSCommonActivatedBy { get; set; }
        public string MicrosoftVSTSTCMAutomatedTestName { get; set; }
        public string MicrosoftVSTSTCMAutomatedTestStorage { get; set; }
        public string MicrosoftVSTSTCMAutomatedTestId { get; set; }
        public string MicrosoftVSTSTCMAutomatedTestType { get; set; }
        public string MicrosoftVSTSTCMAutomationStatus { get; set; }
        public string MicrosoftVSTSTCMSteps { get; set; }
        public string MicrosoftVSTSTCMParameters { get; set; }

        [JsonProperty("Microsoft.VSTS.TCM.LocalDataSource")]
        public string MicrosoftVSTSTCMLocalDataSource { get; set; }
    }
}