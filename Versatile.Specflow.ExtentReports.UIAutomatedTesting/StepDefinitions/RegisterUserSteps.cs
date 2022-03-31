using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Versatile.Specflow.ExtentReports.UIAutomatedTesting.Configurations.Helpers;
using Versatile.Specflow.ExtentReports.UIAutomatedTesting.Pages;

namespace Versatile.Specflow.ExtentReports.UIAutomatedTesting.StepDefinitions
{
    [Binding]
    public class RegisterUserSteps
    {
        private readonly LoginPage Login;
        private RegisterPage Register;
        private readonly User user = new User();

        public RegisterUserSteps(IWebDriver driver) 
            => Login = new LoginPage(driver);

        [Given(@"that the user wants to create an account")]
        public void GivenThatTheUserWantsToCreateAnAccount() 
            => Register = Login.AccessPageLogin(Environments.UrlTst).AccessRegisterUser();


        [Given(@"that the user inform the necessary data for registration ""(.*)"" ""(.*)"" ""(.*)""")]
        public void GivenThatTheUserInformTheNecessaryDataForRegistration(string name, string email, string password) 
            => Register.RegisterUser(name, email, password);

        [Given(@"that the user inform the necessary data for registration")]
        public void GivenThatTheUserInformTheNecessaryDataForRegistration(Table table) 
            => Register.RegisterUser(table.Rows[0]["name"], table.Rows[0]["email"], table.Rows[0]["password"]);

        [Given(@"that the user inform the necessary data for registration")]
        public void GivenThatTheUserInformTheNecessaryDataForRegistration() 
            => Register.RegisterUser(user.Name, user.EmailRegister, user.Password);

        [Then(@"the user is informed that mandatory registration fields have not been filled out ""(.*)"" ""(.*)"" ""(.*)""")]
        public void ThenTheUserIsInformedThatMandatoryRegistrationFieldsHaveNotBeenFilledOut(string name, string email, string password) 
            => Register.ValidateMandatoryFields(name, email, password);

        [Then(@"the user is informed that there is already a registered record for this email")]
        public void ThenTheUserIsInformedThatThereIsAlreadyARegisteredRecordForThisEmail() 
            => Register.ValidateDuplicateRecord();

        [Then(@"the user is informed that the registration was successful")]
        public void ThenTheUserIsInformedThatTheRegistrationWasSuccessful() 
            => Register.VerifyUserRegisteredWithSuccess();
    }
}
