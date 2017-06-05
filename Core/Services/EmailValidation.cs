namespace Core.Services
{
    public class EmailValidation : IEmailValidation
    {
        public bool isValid(string email)
        {
            return !string.IsNullOrEmpty(email) && email.Contains("@");
        }
    }
}
