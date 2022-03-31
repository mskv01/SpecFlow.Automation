using OpenQA.Selenium;
using Versatile.Specflow.ExtentReports.UIAutomatedTesting.Configurations.Factories;
using Versatile.Specflow.ExtentReports.UIAutomatedTesting.Configurations.Helpers;

namespace Versatile.Specflow.ExtentReports.UIAutomatedTesting.Pages
{
    public class RegisterPage : ActionFactory
    {
        public RegisterPage(IWebDriver driver) : base(driver) { }

        public RegisterPage RegisterUser(string name, string email, string password)
        {
            SendKeys(By.Id("nome"), name);
            SendKeys(By.Id("email"), email);
            SendKeys(By.Id("senha"), password);
            return this;
        }

        public void VerifyUserRegisteredWithSuccess()
        {
            Click(By.XPath("//input[@class='btn btn-primary']"));
            ExistsElement(
                By.XPath("//div[contains(text(),'" + Messages.RegisteredUser + "')]"),
                "An error occurred while performing the user registration"
            );
        }

        public void ValidateMandatoryFields(string name, string email, string password)
        {
            Click(By.XPath("//input[@class='btn btn-primary']"));
            if (string.IsNullOrEmpty(name))
            {
                ExistsElement(
                    By.XPath("//div[contains(text(),'Nome é um " + Messages.MandatoryField + "')]"),
                    "No mandatory name message was displayed"
                );
            }
            if (string.IsNullOrEmpty(email))
            {
                ExistsElement(
                    By.XPath("//div[contains(text(),'Email é um " + Messages.MandatoryField + "')]"),
                    "No mandatory email message was displayed"
                );
            }
            if (string.IsNullOrEmpty(password))
            {
                ExistsElement(
                    By.XPath("//div[contains(text(),'Senha é um " + Messages.MandatoryField + "')]"),
                    "No mandatory password message was displayed"
                );
            }
        }

        public void ValidateDuplicateRecord()
        {
            Click(By.XPath("//input[@class='btn btn-primary']"));
            ExistsElement(
                By.XPath("//div[contains(text(),'" + Messages.EmailAlreadyUsed + "')]"),
                "Não foi apresentada mensagem de registro duplicado"
            );
        }

    }
}
