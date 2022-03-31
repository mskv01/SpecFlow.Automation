using OpenQA.Selenium;
using Versatile.Specflow.ExtentReports.UIAutomatedTesting.Configurations.Factories;
using Versatile.Specflow.ExtentReports.UIAutomatedTesting.Configurations.Helpers;

namespace Versatile.Specflow.ExtentReports.UIAutomatedTesting.Pages
{
    public class HomePage : ActionFactory
    {
        public HomePage(IWebDriver driver) : base(driver) { }

        public HomePage ResetMovements()
        {
            Click(By.XPath("//a[contains(text(),'reset')]"));
            ExistsElement(
                By.XPath("//div[contains(text(),'" + Messages.SuccessfullyReset + "')]"),
                "Error when resetting movements!"
            );
            return this;
        }

        public IncludeAccountPage AccessAddAccount()
        {
            Click(By.XPath("//a[contains(text(),'Contas')]"));
            Click(By.XPath("//a[contains(text(),'Adicionar')]"));
            ExistsElement(By.Id("nome"), "Error accessing the add account page");
            return new IncludeAccountPage(Driver());
        }

        public ListAccountPage AccessListAccount()
        {
            Click(By.XPath("//a[contains(text(),'Contas')]"));
            Click(By.XPath("//a[contains(text(),'Listar')]"));
            ExistsElement(By.XPath("//th[contains(text(),'Conta')]"), "Error accessing the list of accounts page");
            return new ListAccountPage(Driver());
        }

    }
}