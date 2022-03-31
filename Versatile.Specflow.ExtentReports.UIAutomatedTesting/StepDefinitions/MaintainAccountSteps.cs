using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Versatile.Specflow.ExtentReports.UIAutomatedTesting.Configurations.Helpers;
using Versatile.Specflow.ExtentReports.UIAutomatedTesting.Pages;

namespace Versatile.Specflow.ExtentReports.UIAutomatedTesting.StepDefinitions
{
    [Binding]
    public class MaintainAccountSteps
    {
        private LoginPage Login;
        private HomePage Home;
        private IncludeAccountPage Include;
        private ChangeAccountPage Change;
        private ListAccountPage List;
        private User user = new User();
        private Account account = new Account();
        private string name;

        public MaintainAccountSteps(IWebDriver driver) => Login = new LoginPage(driver);

        [Given(@"that the user authenticates in the system")]
        public void GivenThatTheUserAuthenticatesInTheSystem() 
            => Home = Login
                .AccessPageLogin(Environments.UrlTst)
                .LogIn(user.Email, user.Password)
                .VerifyAuthenticatedUserWithSuccess();

        [Given(@"that the user accesses the add account screen")]
        public void GivenThatTheUserAccessesTheAddAccountScreen() 
            => Include = Home.ResetMovements().AccessAddAccount();

        [Given(@"that the user inform the necessary data to create the account")]
        public void GivenThatTheUserInformTheNecessaryDataToCreateTheAccount() 
            => Include.IncludeAccount(account.NameAdd);

        [Given(@"that the user inform the necessary data for the creation of the account")]
        public void GivenThatTheUserInformTheNecessaryDataForTheCreationOfTheAccount(Table table) 
            => Include.IncludeAccount(name = table.Rows[0]["name"]);

        [Given(@"that the user accesses the list of account screen")]
        public void GivenThatTheUserAccessesTheListOfAccountScreen() 
            => List = Home.ResetMovements().AccessListAccount();

        [Given(@"that the user accesses the change account screen")]
        public void GivenThatTheUserAccessesTheChangeAccountScreen() 
            => Change = List.AccessAccountChange();

        [Given(@"that the user inform the necessary data to change the account")]
        public void GivenThatTheUserInformTheNecessaryDataToChangeTheAccount()
        {
            Change.ChangeAccount(account.NameUpdate);
        }

        [Given(@"that the user inform the necessary data to change the account")]
        public void GivenThatTheUserInformTheNecessaryDataToChangeTheAccount(Table table) 
            => Change.ChangeAccount(name = table.Rows[0]["name"]);

        [Given(@"that the user requests the deletion of the account")]
        public void GivenThatTheUserRequestsTheDeletionOfTheAccount() 
            => List.DeleteAccount(account.NameDelete);

        [Given(@"that the user requests the exclusion of the account with movement")]
        public void GivenThatTheUserRequestsTheExclusionOfTheAccountWithMovement() 
            => List.DeleteAccountWithTransaction();

        [Then(@"the user is informed that mandatory fields have not been filled in the inclusion")]
        public void ThenTheUserIsInformedThatMandatoryFieldsHaveNotBeenFilledInTheInclusion() 
            => Include.ValidateMandatoryFields(name);

        [Then(@"the user is informed that mandatory fields were not filled in the change")]
        public void ThenTheUserIsInformedThatMandatoryFieldsWereNotFilledInTheChange() 
            => Change.ValidateMandatoryFields();

        [Then(@"the user is informed that there is already a registered account with the same name in the inclusion")]
        public void ThenTheUserIsInformedThatThereIsAlreadyARegisteredAccountWithTheSameNameInTheInclusion() 
            => Include.ValidateDuplicateRecord();


        [Then(@"the user is informed that there is already an account registered with the same name in the change")]
        public void ThenTheUserIsInformedThatThereIsAlreadyAnAccountRegisteredWithTheSameNameInTheChange() 
            => Change.ValidateDuplicateRecord();

        [Then(@"the user is informed that the account has been successfully added")]
        public void ThenTheUserIsInformedThatTheAccountHasBeenSuccessfullyAdded() 
            => Include.VerifyAccountAddedSuccessfully(account.NameAdd);

        [Then(@"the user is informed that the account has been successfully changed")]
        public void ThenTheUserIsInformedThatTheAccountHasBeenSuccessfullyChanged() 
            => Change.VerifyChangeSuccessfully(account.NameUpdate);

        [Then(@"the user is informed that he cannot delete account with movement")]
        public void ThenTheUserIsInformedThatHeCannotDeleteAccountWithMovement() 
            => List.ValidateAccountDeletionWithTransaction();

        [Then(@"the user is informed that the account has been successfully deleted")]
        public void ThenTheUserIsInformedThatTheAccountHasBeenSuccessfullyDeleted() 
            => List.VerifySuccessfullyDeletedAccount();

    }
}
