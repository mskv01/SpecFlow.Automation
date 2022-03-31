using OpenQA.Selenium;
using Versatile.Specflow.ExtentReports.UIAutomatedTesting.Configurations.Factories;
using Versatile.Specflow.ExtentReports.UIAutomatedTesting.Configurations.Helpers;

namespace Versatile.Specflow.ExtentReports.UIAutomatedTesting.Pages
{
    public class LoginPage : ActionFactory
    {
        public LoginPage(IWebDriver driver) : base(driver) { }

        public LoginPage AccessPageLogin(string url)
        {
            Navigate(url);
            ExistsElement(By.Id("email"), "Error accessing the authentication page");
            return this;
        }

        public RegisterPage AccessRegisterUser()
        {
            Click(By.XPath("//a[contains(text(),'Novo usuário?')]"));
            return new RegisterPage(Driver());
        }

        public LoginPage LogIn(string user, string password)
        {
            SendKeys(By.Id("email"), user);
            SendKeys(By.Id("senha"), password);
            return this;
        }

        public HomePage VerifyAuthenticatedUserWithSuccess()
        {
            Click(By.XPath("//button[contains(text(), 'Entrar')]"));
            ExistsElement(
                By.XPath("//div[contains(text(),'" + Messages.LoginReallySuccessful + "')]"),
                "An error occurred while logging in to the system"
            );
            return new HomePage(Driver());
        }

        public void ValidateInvalidLogin()
        {
            Click(By.XPath("//button[contains(text(),'Entrar')]"));
            ExistsElement(
                By.XPath("//div[contains(text(),'" + Messages.InvalidLogin + "')]"),
                "Invalid login message was not displayed"
            );
        }

        public void ValidateMandatoryFields(string email, string password)
        {
            Click(By.XPath("//button[contains(text(),'Entrar')]"));
            if (string.IsNullOrEmpty(email))
            {
                ExistsElement(
                    By.XPath("//div[contains(text(), 'Email é um " + Messages.MandatoryField + "')]"),
                    "No mandatory email message was displayed"
                );
            }
            if (string.IsNullOrEmpty(password))
            {
                ExistsElement(
                    By.XPath("//div[contains(text(), 'Senha é um " + Messages.MandatoryField + "')]"),
                    "No mandatory password message was displayed"
                );
            }
        }

    }
}