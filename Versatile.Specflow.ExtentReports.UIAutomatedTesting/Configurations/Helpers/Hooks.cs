using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using System;
using System.IO;
using TechTalk.SpecFlow;
using Versatile.Specflow.ExtentReports.CryptoEngine.AppSettings;
using Versatile.Specflow.ExtentReports.UIAutomatedTesting.Configurations.Factories;

namespace Versatile.Specflow.ExtentReports.UIAutomatedTesting.Configurations.Helpers
{
    [Binding]
    public class Hooks
    {
        private readonly IObjectContainer _objectContainer;
        private static IWebDriver _driver;
        private static DriverFactory _driverFactory;

        private static ExtentTest _feature;
        private static ExtentTest _scenario;
        private static AventStack.ExtentReports.ExtentReports _extent;
        private static readonly string PathReport = SystemProperties.PathProject + "\\TestResults\\Report\\ExtentReport.html";



        public Hooks(IObjectContainer objectContainer) => _objectContainer = objectContainer;

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            Directory.CreateDirectory(SystemProperties.PathProject + Path.Combine("\\TestResults\\Report"));
            Directory.CreateDirectory(SystemProperties.PathProject + Path.Combine("\\TestResults\\Img"));
            _driverFactory = new DriverFactory();
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
            _driver = _driverFactory.CreateDriver(Browsers.CHROME);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _driver.Manage().Window.Maximize();
            _objectContainer.RegisterInstanceAs(_driver);
            _scenario = _feature.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);
            _scenario.AssignCategory(scenarioContext.ScenarioInfo.Tags);
        }

        [AfterStep]
        public static void InsertReportingSteps(ScenarioContext scenarioContext)
        {
            var ScreenshotFilePath = Path.Combine(SystemProperties.PathProject + "\\TestResults\\Img", Path.GetFileNameWithoutExtension(Path.GetTempFileName()) + ".png");
            var mediaModel = MediaEntityBuilder.CreateScreenCaptureFromPath(ScreenshotFilePath).Build();

            if (scenarioContext.TestError != null)
            {
                _driver.TakeScreenshot().SaveAsFile(ScreenshotFilePath, ScreenshotImageFormat.Png);
                switch (ScenarioStepContext.Current.StepInfo.StepDefinitionType)
                {
                    case TechTalk.SpecFlow.Bindings.StepDefinitionType.Given:
                        _scenario.CreateNode<Given>("Given " + ScenarioStepContext.Current.StepInfo.Text).Fail(scenarioContext.TestError.Message, mediaModel);
                        break;

                    case TechTalk.SpecFlow.Bindings.StepDefinitionType.When:
                        _scenario.CreateNode<When>("When " + ScenarioStepContext.Current.StepInfo.Text).Fail(scenarioContext.TestError.Message, mediaModel);
                        break;

                    case TechTalk.SpecFlow.Bindings.StepDefinitionType.Then:
                        _scenario.CreateNode<Then>("Then " + ScenarioStepContext.Current.StepInfo.Text).Fail(scenarioContext.TestError.Message, mediaModel);
                        break;
                }
            }

            if (scenarioContext.ScenarioExecutionStatus == ScenarioExecutionStatus.StepDefinitionPending)
            {
                switch (ScenarioStepContext.Current.StepInfo.StepDefinitionType)
                {
                    case TechTalk.SpecFlow.Bindings.StepDefinitionType.Given:
                        _scenario.CreateNode<Given>("Given " + ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending", mediaModel);
                        break;

                    case TechTalk.SpecFlow.Bindings.StepDefinitionType.When:
                        _scenario.CreateNode<When>("When " + ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending", mediaModel);
                        break;

                    case TechTalk.SpecFlow.Bindings.StepDefinitionType.Then:
                        _scenario.CreateNode<Then>("Then " + ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending", mediaModel);
                        break;
                }
            }

            if (scenarioContext.TestError == null)
            {
                _driver.TakeScreenshot().SaveAsFile(ScreenshotFilePath, ScreenshotImageFormat.Png);
                switch (ScenarioStepContext.Current.StepInfo.StepDefinitionType)
                {
                    case TechTalk.SpecFlow.Bindings.StepDefinitionType.Given:
                        _scenario.CreateNode<Given>("Given " + ScenarioStepContext.Current.StepInfo.Text).Pass(string.Empty, mediaModel);
                        break;

                    case TechTalk.SpecFlow.Bindings.StepDefinitionType.When:
                        _scenario.CreateNode<When>("When " + ScenarioStepContext.Current.StepInfo.Text).Pass(string.Empty, mediaModel);
                        break;

                    case TechTalk.SpecFlow.Bindings.StepDefinitionType.Then:
                        _scenario.CreateNode<Then>("Then " + ScenarioStepContext.Current.StepInfo.Text).Pass(string.Empty, mediaModel);
                        break;
                }
            }
        }

        [AfterScenario]
        public void AfterScenario(ScenarioContext scenarioContext)
        {
            _extent.Flush();
            _driver?.Quit();
            _driver?.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}