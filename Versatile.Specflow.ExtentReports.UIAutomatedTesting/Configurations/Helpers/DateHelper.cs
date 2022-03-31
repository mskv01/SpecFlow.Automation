using System;

namespace Versatile.Specflow.ExtentReports.UIAutomatedTesting.Configurations.Helpers
{
    public class DateHelper
    {
        public static string ReturnDateHours() => DateTime.Now.ToString("yyyyMMddhhmmss");

        public static string ReturnDateHoursFormated() => DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

        public static string ReturnDate() => DateTime.Now.ToString("yyyy-MM-dd");

        public static string ReturnDatePtBr() => DateTime.Now.ToString("dd/MM/yyyy");
    }
}
