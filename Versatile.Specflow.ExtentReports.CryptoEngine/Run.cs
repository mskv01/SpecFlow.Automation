using NUnit.Framework;
using System;
using Versatile.Specflow.ExtentReports.CryptoEngine.CryptoEngine;

namespace Versatile.Specflow.ExtentReports.CryptoEngine
{
    public class Run
    {

        [Test]
        public void Encrypt() 
            => Console.WriteLine(Crypto.Encrypt("YOUR ACCESS TOKEN", "Cybernetics CRM 4.0 Word Addin"));

    }
}
