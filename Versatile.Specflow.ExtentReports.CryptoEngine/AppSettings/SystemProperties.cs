using System;

namespace Versatile.Specflow.ExtentReports.CryptoEngine.AppSettings
{
    public class SystemProperties
    {
        public static string PathProject = AppDomain.CurrentDomain.BaseDirectory.ToString().Remove(AppDomain.CurrentDomain.BaseDirectory.ToString().LastIndexOf("\\") - 23);
    }
}