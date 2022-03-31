using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;

namespace Versatile.Specflow.ExtentReports.UIAutomatedTesting.Configurations.Factories
{
    public class DriverFactory
    {
        private readonly IWebDriver _driver;

        public DriverFactory() { }

        public DriverFactory(IWebDriver driver) => _driver = driver;

        public IWebDriver CreateDriver(string browser = null)
        {
            browser ??= "CHROME";

            switch (browser.ToUpperInvariant())
            {
                case "CHROME":
                    return new ChromeDriver();
                case "FIREFOX":
                    return new FirefoxDriver();
                case "IE":
                    return new InternetExplorerDriver();
                default:
                    throw new ArgumentException($"Browser not yet implemented: {browser}");
            }
        }

        public void Navigate(string url) => _driver.Navigate().GoToUrl(url);

        public IWebDriver Driver() => _driver;

    }
}