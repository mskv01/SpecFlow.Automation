using NUnit.Framework;
using Versatile.Specflow.ExtentReports.AzureTestAssociation.Helpers;

namespace Versatile.Specflow.ExtentReports.AzureTestAssociation.AzureTestCaseAssociation
{
    public class AzureTestCaseAssociation
    {

        [Test]
        public void TestCaseAssociation()
        {
            // Associate Test Case
            TestCaseAssociationHelper.TestCaseAssociate(
                79,
                "Versatile.Specflow.ExtentReports.UIAutomatedTesting.dll",
                "Versatile.Specflow.ExtentReports.UIAutomatedTesting.Features._01CadastrarUsuarioFeature._02ValidarE_MailJaCadastrado"
            );

            // Clear association
            // TestCaseAssociationHelper.ClearTestCase(79);
        }

    }
}