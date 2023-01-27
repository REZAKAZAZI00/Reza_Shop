
namespace Shop.Core.Convertors
{
    public class FixedText
    {
        public static string FexedEmail(string email) 
        {
            return email.Trim().ToLower();
        }
    }
}
