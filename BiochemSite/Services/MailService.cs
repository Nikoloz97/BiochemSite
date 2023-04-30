namespace BiochemSite.Services
{
    // Send a email message to me when a chapter/subchapter was modified 
    public class MailService
    {
        private string _mailTo = "nick.gotsy@gmail.com";
        private string _mailFrom = "nick.gotsy@gmail.com";

        public void Send(string subject, string message)
        {
            // Mimicking creating an email by just writing to console 
            Console.WriteLine($"Mail from {_mailFrom} to {_mailTo}, " + $"using {nameof(MailService)}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message : {message}");
        }
    }
}
