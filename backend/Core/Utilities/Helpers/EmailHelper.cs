using System.Net;
using System.Net.Mail;

namespace Core.Utilities.Helpers
{
    public class EmailHelper
    {
        public static bool SendEmail(string to, string subject, string body)
        {
            try
            {
                using (var client = new SmtpClient("smtp.gmail.com", 587))
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential("fth123bng@gmail.com", "bovaeyyqummfnbem");

                    var message = new MailMessage();
                    message.From = new MailAddress("fth123bng@gmail.com");
                    message.To.Add(to);
                    message.Subject = subject;
                    message.Body = body;
                    message.IsBodyHtml = true;

                    client.Send(message);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Mail gönderme hatası: {ex.Message}");
                return false;
            }
        }
    }
}
