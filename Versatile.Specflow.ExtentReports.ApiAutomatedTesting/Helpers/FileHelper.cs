using System;
using System.IO;
using System.Text;
using Versatile.Specflow.ExtentReports.CryptoEngine.AppSettings;

namespace Versatile.Specflow.ExtentReports.ApiAutomatedTesting.Helpers
{
    public class FileHelper
    {
        private static readonly string PathFile = SystemProperties.PathProject + "TestResults\\Files\\";

        public static string ReadFile(string filename)
        {
            try
            {
                return File.ReadAllText(Directory.CreateDirectory(PathFile) + filename);
            } 
            catch (Exception)
            {
                return null;
            }
        }

        public static void CreateFile(string fileName, string text) 
            => File.WriteAllText(Directory.CreateDirectory(PathFile) + fileName, text, Encoding.UTF8);
    }
}