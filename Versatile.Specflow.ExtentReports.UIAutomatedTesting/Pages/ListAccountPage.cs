using OpenQA.Selenium;
using Versatile.Specflow.ExtentReports.UIAutomatedTesting.Configurations.Factories;
using Versatile.Specflow.ExtentReports.UIAutomatedTesting.Configurations.Helpers;

namespace Versatile.Specflow.ExtentReports.UIAutomatedTesting.Pages
{
    public class ListAccountPage : TableFactory
    {
        public ListAccountPage(IWebDriver driver) : base(driver) { }

        /// <summary>
        /// Method to access the account change screen with the name "Conta para alterar"
        /// </summary>
        public ChangeAccountPage AccessAccountChange()
        {
            int index = 0;
            foreach (IWebElement tr in ReturnTrs())
            {
                if (ReturnTd(tr, 0).Text.Contains("Conta para alterar"))
                {
                    Click(ReturnButtonLink(ReturnTd(tr, 1), 0));
                    break;
                }
                VerifyIfIsLastRegister(index++, "The account with the name was not found 'Conta para alterar'");
            }
            return new ChangeAccountPage(Driver());
        }

        /// <summary>
        /// Method for requesting deletion of account in use with the name"Conta com movimentacao"
        /// </summary>
        public void DeleteAccountWithTransaction()
        {
            int index = 0;
            foreach (IWebElement tr in ReturnTrs())
            {
                if (ReturnTd(tr, 0).Text.Contains("Conta com movimentacao"))
                {
                    Click(ReturnButtonLink(ReturnTd(tr, 1), 1));
                    break;
                }
                VerifyIfIsLastRegister(index++, "The account with the name was not found 'Conta com movimentacao'");
            }
        }

        /// <summary>
        /// Method for validating account deletion in use of the account with the name "Conta com movimentacao"
        /// </summary>
        public void ValidateAccountDeletionWithTransaction()
        {
            ExistsElement(
                By.XPath("//div[contains(text(),'" + Messages.AccountWithMovements + "')]"),
                "There was no message for deleting account not allowed!"
            );
        }

        /// <summary>
        /// Method to delete account
        /// </summary>
        public void DeleteAccount(string account)
        {
            for (int i = 0; i < ReturnTrs().Count; i++)
            {
                if (ReturnTd(ReturnTr(i), 0).Text.Equals(account))
                {
                    Click(ReturnButtonLink(ReturnTd(ReturnTr(i), 1), 1));
                    return;
                }
            }

        }

        /// <summary>
        /// Method for verifying account deletion
        /// </summary>
        public void VerifySuccessfullyDeletedAccount()
        {
            ExistsElement(
                By.XPath("//div[contains(text(),'" + Messages.AccountRemovedSuccessfully + "')]"),
                "No successful deletion message was displayed!"
            );
        }

    }
}