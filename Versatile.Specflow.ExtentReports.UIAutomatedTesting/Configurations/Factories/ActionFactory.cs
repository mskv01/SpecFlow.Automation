using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Versatile.Specflow.ExtentReports.UIAutomatedTesting.Configurations.Factories
{
    public class ActionFactory : DriverFactory
    {
        public ActionFactory(IWebDriver driver) : base(driver) { }

        /// <summary>
        /// Method waiting for the element to exist
        /// </summary>
        public IWebElement FindElement(By by, int timeout = 10) => timeout > 0
            ? new WebDriverWait(Driver(), TimeSpan.FromSeconds(timeout)).Until(drv => drv.FindElement(by))
            : Driver().FindElement(by);

        /// <summary>
        /// Method waiting for the element to exist
        /// </summary>
        public ReadOnlyCollection<IWebElement> FindElements(By by, int timeout = 10) => timeout > 0
            ? new WebDriverWait(Driver(), TimeSpan.FromSeconds(timeout)).Until(drv => drv.FindElements(by))
            : Driver().FindElements(by);

        /// <summary>
        /// Method that checks if an element exists
        /// </summary>
        public bool ExistsElement(By by, int timeout = 10)
        {
            try
            {
                FindElement(by, timeout);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void ExistsElement(By by, string text, int timeout = 10)
        {
            if (!ExistsElement(by, timeout)) Assert.Fail(text);
        }

        /// <summary>
        /// Method that returns elements collection
        /// </summary>
        public ReadOnlyCollection<IWebElement> CollectionElements(By by, int timeout = 10) => FindElements(by, timeout);

        /// <summary>
        /// Method for cleaning an element
        /// </summary>
        public void Clear(By by) => FindElement(by).Clear();

        /// <summary>
        /// Method to insert text in an element
        /// </summary>
        public void SendKeys(By by, string text, int timeout = 10)
        {
            if (!string.IsNullOrEmpty(text)) FindElement(by, timeout).SendKeys(text);
        }

        /// <summary>
        /// Method to insert text in an element
        /// </summary>
        public void SendKeys(IWebElement element, string text)
        {
            if (!string.IsNullOrEmpty(text)) element.SendKeys(text);
        }

        /// <summary>
        /// Method for clicking on an element
        /// </summary>
        public void Click(By by, int timeout = 10) => FindElement(by, timeout).Click();

        /// <summary>
        /// Method for clicking on an element
        /// </summary>
        public void Click(IWebElement element) => element.Click();

        /// <summary>
        /// Method for clicking on an element by index
        /// </summary>
        public void ClickByIndex(By by, int index, int timeout = 10) => Click(CollectionElements(by, timeout)[index]);

        /// <summary>
        /// Method for selecting element by text
        /// </summary>
        public void Select(By by, string text, int timeout = 10) => new SelectElement(FindElement(by, timeout)).SelectByText(text);

        /// <summary>
        /// Method for selecting element by index
        /// </summary>
        public void Select(By by, int index, int timeout = 10) => new SelectElement(FindElement(by, timeout)).SelectByIndex(index);

        /// <summary>
        /// Method that returns the text of the element
        /// </summary>
        public string ReturnText(By by, int timeout = 10) => FindElement(by, timeout).Text;

        /// <summary>
        /// Method that returns the text of the element by index
        /// </summary>
        public string ReturnTextByIndex(By by, int index, int timeout = 10) => CollectionElements(by, timeout)[index].Text;

        /// <summary>
        /// Method that returns the text of the element by attribute
        /// </summary>
        public string ReturnTextByAttribute(By by, string attr, int timeout = 10) => FindElement(by, timeout).GetAttribute(attr);

        /// <summary>
        /// Method that checks whether current text is the same as expected text
        /// </summary>
        public void VerifyText(string expectedMessage, string currentMessage, string errorMessage) => Assert.AreEqual(expectedMessage, currentMessage, errorMessage);

        /// <summary>
        /// Method that checks if element's text contains the expected text
        /// </summary>
        public void ContainsText(string expectedMessage, string currentMessage, string errorMessage) => Assert.IsTrue(currentMessage.Contains(expectedMessage), errorMessage);

        /// <summary>
        /// Method that chages the browser tab
        /// </summary>
        public IWebDriver ChangeTab(string tab = null) => !string.IsNullOrEmpty(tab) && tab.Equals("Last")
            ? Driver().SwitchTo().Window(Driver().WindowHandles.Last())
            : Driver().SwitchTo().Window(Driver().WindowHandles.First());

        /// <summary>
        /// Method to wait for the angular page to load
        /// </summary>
        public void WaitAngularPageLoad(int timeout = 10) => new WebDriverWait(Driver(), TimeSpan.FromSeconds(timeout)).Until(wd => ((IJavaScriptExecutor)Driver()).ExecuteScript("return (window.angular !== undefined) && (angular.element(document).injector() !== undefined) && (angular.element(document).injector().get('$http').pendingRequests.length === 0)").ToString());
        
        /// <summary>
        /// Method to wait for the page to load
        /// </summary>
        public void WaitPageLoad(int timeout = 10) => new WebDriverWait(Driver(), TimeSpan.FromSeconds(timeout)).Until(wd => ((IJavaScriptExecutor)Driver()).ExecuteScript("return document.readyState").Equals("complete"));

        /// <summary>
        /// Method to zoom in
        /// </summary>
        public void ZoomIn() => new Actions(Driver()).SendKeys(Keys.Control).SendKeys(Keys.Add).Perform();

        /// <summary>
        /// Method to zoom out
        /// </summary>
        public void ZoomOut() => new Actions(Driver()).SendKeys(Keys.Control).SendKeys(Keys.Subtract).Perform();

        /// <summary>
        /// Method to zoom by percentage
        /// </summary>
        public void ZoomPercentage(int percent) => ((IJavaScriptExecutor)Driver()).ExecuteScript("document.body.style.zoom = ' " + percent + "%';");

        /// <summary>
        /// Method that induces mouse hover
        /// </summary>
        public void MouseHouver(IWebElement element) => new Actions(Driver()).MoveToElement(element).Perform();

        /// <summary>
        /// Method to scroll to the element
        /// </summary>
        public void ScrollToElement(IWebElement element, int up = 0, int down = 0) => ((IJavaScriptExecutor)Driver()).ExecuteScript("arguments[0].scrollIntoView(true);window.scrollBy(" + up + "," + down + ")", element);

        /// <summary>
        /// Method to accept alert
        /// </summary>
        public void AcceptAlert() => Driver().SwitchTo().Alert().Accept();

    }
}