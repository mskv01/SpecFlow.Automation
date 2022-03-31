using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace Versatile.Specflow.ExtentReports.UIAutomatedTesting.Configurations.Factories
{
    public class TableFactory : ActionFactory
    {
        public TableFactory(IWebDriver driver) : base(driver) { }

        /// <summary>
        /// Method for updating the tabl after performing a record inactivation, activation or deletion
        /// </summary>
        public TableFactory UpdateTable() => new TableFactory(Driver());

        /// <summary>
        ///  Method that returns all <tr> records in the table
        /// </summary>
        /// <param name="tableIndex">Optional field if you have more than one table on the screen</param>
        public ReadOnlyCollection<IWebElement> ReturnTrs(int tableIndex = 0) => CollectionElements(By.TagName("tbody"))[tableIndex].FindElements(By.TagName("tr"));

        /// <summary>
        ///  Method that returns all <tr> records in the table
        /// </summary>
        /// <param name="tableIndex">Optional field if you have more than one table on the screen</param>
        public ReadOnlyCollection<IWebElement> ReturnTrs(ReadOnlyCollection<IWebElement> elements, int tableIndex = 0) => elements[tableIndex].FindElements(By.TagName("tbody"))[tableIndex].FindElements(By.TagName("tr"));


        /// <summary>
        /// Method that returns only one <tr> record according to the given index 
        /// </summary>
        /// <param name="tableIndex">Optional field if you have more than one table on the screen</param>
        public IWebElement ReturnTr(int index, int tableIndex = 0) => ReturnTrs(tableIndex)[index];

        /// <summary>
        /// Method that returns the <td> column of the <tr> according to the given index 
        /// </summary>
        public IWebElement ReturnTd(IWebElement tr, int index) => tr.FindElements(By.TagName("td"))[index];

        /// <summary>
        /// Method that returns the <button> of the <td> according to the given index 
        /// </summary>
        public IWebElement ReturnButton(IWebElement column, int index) => column.FindElements(By.TagName("button"))[index];

        /// <summary>
        /// Method that returns the <span> of the <td> according to the given index 
        /// </summary>
        public IWebElement ReturnSpan(IWebElement column, int index) => column.FindElements(By.TagName("span"))[index];

        /// <summary>
        /// Method that return the <a> link from <td> according to the given index 
        /// </summary>
        public IWebElement ReturnButtonLink(IWebElement column, int index) => column.FindElements(By.TagName("a"))[index];

        /// <summary>
        /// Method that checks whether all records have been viewed and displays the error message
        /// </summary>
        public void VerifyIfIsLastRegister(int index, string errorMessage)
        {
            if (ReturnTrs().Count == (index + 1)) Assert.Fail(errorMessage);
        }

    }
}
