namespace Versatile.Specflow.ExtentReports.UIAutomatedTesting.Configurations.Helpers
{
    public class User
    {
        public User()
        {
            Name = "Fabio Alves";
            Email = "fabioaraujo.alves@email.com";
            EmailRegister = "fabioaraujo.alves@email.com" + DateHelper.ReturnDateHours();
            Password = "123456";
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string EmailRegister { get; set; }
        public string Password { get; set; }

    }
}