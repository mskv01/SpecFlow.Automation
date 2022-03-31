using OpenQA.Selenium;
using Versatile.Specflow.ExtentReports.UIAutomatedTesting.Configurations.Factories;
using Versatile.Specflow.ExtentReports.UIAutomatedTesting.Configurations.Helpers;

namespace Versatile.Specflow.ExtentReports.UIAutomatedTesting.Pages
{
    public class IncludeAccountPage : TableFactory
    {
        public IncludeAccountPage(IWebDriver driver) : base(driver) { }

        public ListAccountPage AccessAccountList()
        {
            Click(By.XPath("//a[contains(text(),'Contas')]"));
            Click(By.XPath("//a[contains(text(),'Listar')]"));
            ExistsElement(By.XPath("//th[contains(text(),'Conta')]"), "Error accessing the list of accounts page");
            return new ListAccountPage(Driver());
        }

        public void IncludeAccount(string account)
        {
            SendKeys(By.Id("nome"), account);
        }

        public void VerifyAccountAddedSuccessfully(string account)
        {
            Click(By.XPath("//button[@class='btn btn-primary']"));
            ExistsElement(
                By.XPath("//div[contains(text(),'" + Messages.AccountAddedSuccessfully + "')]"),
                "No account message successfully added!"
            );
            ValidateAccountInclusion(account);
        }

        public void ValidateMandatoryFields(string name)
        {
            Click(By.XPath("//button[@class='btn btn-primary']"));
            if (string.IsNullOrEmpty(name))
            {
                ExistsElement(
                    By.XPath("//div[contains(text(),'" + Messages.MandatoryAccount + "')]"),
                    "No mandatory account name message was displayed"
                );
            }
        }

        public void ValidateDuplicateRecord()
        {
            Click(By.XPath("//button[@class='btn btn-primary']"));
            ExistsElement(
                By.XPath("//div[contains(text(),'" + Messages.AccountIncluded + "')]"),
                "No duplicate registration message was displayed"
            );
        }

        public void ValidateAccountInclusion(string account)
        {
            int index = 0;
            foreach (IWebElement tr in ReturnTrs())
            {
                string accountname = ReturnTd(tr, 0).Text;
                if (accountname.Equals(account)) return;
                VerifyIfIsLastRegister(index++, "Account was not added correctly");
            }
        }

    }
}
