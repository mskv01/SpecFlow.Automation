using OpenQA.Selenium;
using Versatile.Specflow.ExtentReports.UIAutomatedTesting.Configurations.Factories;
using Versatile.Specflow.ExtentReports.UIAutomatedTesting.Configurations.Helpers;

namespace Versatile.Specflow.ExtentReports.UIAutomatedTesting.Pages
{
    public class ChangeAccountPage : TableFactory
    {
        public ChangeAccountPage(IWebDriver driver) : base(driver) { }

        public void ChangeAccount(string account)
        {
            Clear(By.Id("nome"));
            SendKeys(By.Id("nome"), account);
        }

        public void VerifyChangeSuccessfully(string account)
        {
            Click(By.XPath("//button[@class='btn btn-primary']"));
            ExistsElement(
                By.XPath("//div[contains(text(),'" + Messages.AccountChangedSuccessfully + "')]"),
                "No account message successfully changed!"
            );
            ValidateAccountChange(account);
        }

        public void ValidateMandatoryFields()
        {
            Clear(By.Id("nome"));
            Click(By.XPath("//button[@class='btn btn-primary']"));
            ExistsElement(
                By.XPath("//div[contains(text(),'" + Messages.MandatoryAccount + "')]"),
                "No mandatory account name message was displayed"
            );
        }

        public void ValidateDuplicateRecord()
        {
            Click(By.XPath("//button[@class='btn btn-primary']"));
            ExistsElement(
                By.XPath("//div[contains(text(),'" + Messages.AccountIncluded + "')]"),
                "No duplicate registration message was displayed"
            );
        }

        public void ValidateAccountChange(string account)
        {
            for (int index = 0; index < ReturnTrs().Count; index++)
            {
                string accountname = ReturnTd(ReturnTr(index), 0).Text;
                if (accountname.Equals(account)) return;
                VerifyIfIsLastRegister(index, "Account was not changed correctly");
            }
        }

    }
}