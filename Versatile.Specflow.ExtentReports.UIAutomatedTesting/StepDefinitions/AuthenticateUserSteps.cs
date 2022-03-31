using OpenQA.Selenium;
using System.Data;
using TechTalk.SpecFlow;
using Versatile.Specflow.ExtentReports.AzureTestParams;
using Versatile.Specflow.ExtentReports.UIAutomatedTesting.Configurations.Helpers;
using Versatile.Specflow.ExtentReports.UIAutomatedTesting.Pages;

namespace Versatile.Specflow.ExtentReports.UIAutomatedTesting.StepDefinitions
{
    [Binding]
    public class AuthenticateUserSteps
    {
        private readonly LoginPage Login;
        private readonly User user = new User();
        private readonly AzureParams azureParams = new AzureParams();
        private readonly DataRowCollection data;

        public AuthenticateUserSteps(IWebDriver driver)
        {
            Login = new LoginPage(driver);
            data = azureParams.GetParams("76");
        }

        [Given(@"that the user wants to authenticate")]
        public void GivenThatTheUserWantsToAuthenticate() 
            => Login.AccessPageLogin(Environments.UrlTst);

        [Given(@"that the user enter the necessary data for authentication ""(.*)"" ""(.*)""")]
        public void GivenThatTheUserEnterTheNecessaryDataForAuthentication(string email, string password) 
            => Login.LogIn(email, password);

        [Given(@"that the user inform the necessary data for authentication")]
        public void GivenThatTheUserInformTheNecessaryDataForAuthentication(Table table) 
            => Login.LogIn(data[0]["email"].ToString(), data[0]["senha"].ToString());

        [Given(@"that the user inform the necessary data for authentication")]
        public void GivenThatTheUserInformTheNecessaryDataForAuthentication() 
            => Login.LogIn(user.Email, user.Password);

        [Then(@"the user is informed that mandatory fields have not been filled out ""(.*)"" ""(.*)""")]
        public void ThenTheUserIsInformedThatMandatoryFieldsHaveNotBeenFilledOut(string email, string password) 
            => Login.ValidateMandatoryFields(email, password);

        [Then(@"the user is informed that authentication has not been carried out")]
        public void ThenTheUserIsInformedThatAuthenticationHasNotBeenCarriedOut() 
            => Login.ValidateInvalidLogin();

        [Then(@"the user is informed that the authentication was successful")]
        public void ThenTheUserIsInformedThatTheAuthenticationWasSuccessful() 
            => Login.VerifyAuthenticatedUserWithSuccess();

    }
}
