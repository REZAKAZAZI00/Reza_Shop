
namespace Shop.Core.Sender
{
    public class SendEmail
    {
        public static void Send(string to,string subject,string body)
        {
            MailMessage mail = new();
            SmtpClient SmtpServer = new("smtp.gmail.com");
            mail.From = new MailAddress("m3979460258@gmail.com", "فروشگاه");
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            //System.Net.Mail.Attachment attachment;
            // attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
            // mail.Attachments.Add(attachment);

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("m3979460258@gmail.com", "@Reza1381");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);

        }
    }
}