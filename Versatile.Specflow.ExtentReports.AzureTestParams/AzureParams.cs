using System.Data;
using System.Threading.Tasks;
using Versatile.Specflow.ExtentReports.CryptoEngine.CryptoEngine;

namespace Versatile.Specflow.ExtentReports.AzureTestParams
{
    public class AzureParams
    {
        private static readonly string _vstsPat = "/VcGGZ+g3OX4nqKQAM32byqU0ha0yjZ5go2zA3bLhvX/WYME4kx+qqL0SKGVbiIg0G+LNXBZzliJAPlXkYOgJg==";
        private static readonly string _cryptoKey = "Cybernetics CRM 4.0 Word Addin";
        private static readonly string _vstsUrl = "https://fabiotestproject.visualstudio.com/";

        public DataRowCollection GetParams(string testcaseID)
        {
            GetTestCaseParams p = new GetTestCaseParams
            {
                Pat = Crypto.Decrypt(_vstsPat, _cryptoKey),
                VstsURI = _vstsUrl
            };
            DataSet ds = new DataSet();
            Task.Run(async () => { ds = await p.GetParams(testcaseID); }).GetAwaiter().GetResult();
            return ds.Tables[0].Rows;
        }

    }
}