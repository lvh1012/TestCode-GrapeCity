using System.ComponentModel.DataAnnotations;

namespace TestCode.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class EmailAttribute : ValidationAttribute
    {
        public EmailAttribute() : base("Email field validation failed")
        {
        }

        public override bool IsValid(object value)
        {
            var email = (String)value;
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
    }
}
