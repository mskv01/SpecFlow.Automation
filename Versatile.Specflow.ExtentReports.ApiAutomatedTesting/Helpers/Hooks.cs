using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using System;
using TechTalk.SpecFlow;
using Versatile.Specflow.ExtentReports.CryptoEngine.AppSettings;

namespace Versatile.Specflow.ExtentReports.ApiAutomatedTesting.Helpers
{
    [Binding]
    public class Hooks
    {
        private static ExtentTest _feature;
        private static ExtentTest _scenario;
        private static AventStack.ExtentReports.ExtentReports _extent;
        public static string _response = string.Empty;
        //public static string _result = SystemProperties.PathProject + "\\TestResults\\Files\\result.txt";
        private static readonly string PathReport = SystemProperties.PathProject + "\\TestResults\\Report\\ExtentReport.html";
        

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            var reporter = new ExtentHtmlReporter(PathReport);
            _extent = new AventStack.ExtentReports.ExtentReports();
            _extent.AttachReporter(reporter);
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext) =>
            _feature = _extent.CreateTest<Feature>(featureContext.FeatureInfo.Title);

        [BeforeScenario]
        public void BeforeScenario(ScenarioContext scenarioContext)
        {
            _scenario = _feature.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);
            _scenario.AssignCategory(scenarioContext.ScenarioInfo.Tags);
        }

        [AfterStep]
        public static void InsertReportingSteps(ScenarioContext scenarioContext)
        {
            if (scenarioContext.TestError != null)
            {
                switch (ScenarioStepContext.Current.StepInfo.StepDefinitionType)
                {
                    case TechTalk.SpecFlow.Bindings.StepDefinitionType.Given:
                        _scenario.CreateNode<Given>("Given " + ScenarioStepContext.Current.StepInfo.Text).Fail(scenarioContext.TestError.Message);
                        break;

                    case TechTalk.SpecFlow.Bindings.StepDefinitionType.When:
                        _scenario.CreateNode<When>("When " + ScenarioStepContext.Current.StepInfo.Text).Fail(scenarioContext.TestError.Message);
                        break;

                    case TechTalk.SpecFlow.Bindings.StepDefinitionType.Then:
                        _scenario.CreateNode<Then>("Then " + ScenarioStepContext.Current.StepInfo.Text).Fail(scenarioContext.TestError.Message);
                        break;
                }
            }

            if (scenarioContext.ScenarioExecutionStatus == ScenarioExecutionStatus.StepDefinitionPending)
            {
                switch (ScenarioStepContext.Current.StepInfo.StepDefinitionType)
                {
                    case TechTalk.SpecFlow.Bindings.StepDefinitionType.Given:
                        _scenario.CreateNode<Given>("Given " + ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                        break;

                    case TechTalk.SpecFlow.Bindings.StepDefinitionType.When:
                        _scenario.CreateNode<When>("When " + ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                        break;

                    case TechTalk.SpecFlow.Bindings.StepDefinitionType.Then:
                        _scenario.CreateNode<Then>("Then " + ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                        break;
                }
            }

            if (scenarioContext.TestError == null)
            {
                switch (ScenarioStepContext.Current.StepInfo.StepDefinitionType)
                {
                    case TechTalk.SpecFlow.Bindings.StepDefinitionType.Given:
                        _scenario.CreateNode<Given>("Given " + ScenarioStepContext.Current.StepInfo.Text).Pass(string.Empty);
                        break;

                    case TechTalk.SpecFlow.Bindings.StepDefinitionType.When:
                        _scenario.CreateNode<When>("When " + ScenarioStepContext.Current.StepInfo.Text).Pass(string.Empty);
                        break;

                    case TechTalk.SpecFlow.Bindings.StepDefinitionType.Then:
                        _scenario.CreateNode<Then>("Then " + ScenarioStepContext.Current.StepInfo.Text).Log(Status.Pass,
                            "<strong>" + _response + "</strong>"
                        );
                        break;

                    /*case TechTalk.SpecFlow.Bindings.StepDefinitionType.Then:
                       _scenario.CreateNode<Then>("Then " + ScenarioStepContext.Current.StepInfo.Text).Log(Status.Pass,
                           "<a href='" + _result + "' target='_blank'>Result</a>"
                       );
                        break;*/
                }
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _extent.Flush();
            GC.SuppressFinalize(this);
        }

    }
}