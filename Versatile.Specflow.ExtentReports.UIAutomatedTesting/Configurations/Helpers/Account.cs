namespace Versatile.Specflow.ExtentReports.UIAutomatedTesting.Configurations.Helpers
{
    public class Account
    {
        public Account()
        {
            NameAdd = "Água " + DateHelper.ReturnDateHours();
            NameUpdate = "Luz " + DateHelper.ReturnDateHours();
            NameDelete = "Conta para alterar";
        }

        public string NameAdd { get; set; }
        public string NameUpdate { get; set; }
        public string NameDelete { get; set; }
    }
}
