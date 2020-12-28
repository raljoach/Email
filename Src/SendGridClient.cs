using System;

namespace Email
{
    public class SendGridClient : IEmailClient
    {
        private string apiKey;		
		public class SendGridClient(string apiKey)
		{
			this.apiKey = apiKey;
		}
        public async Task Send(EmailData email)
        {			            
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress(email.FromEmail, email.FromName);
                var subject = email.Subject;
                var to = new EmailAddress(email.Recipients[0].Email, email.Recipients[0].Name);
                var plainTextContent = email.TextBody;
                var htmlContent = email.HtmlBody;
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);
                Console.WriteLine(response.StatusCode);
                Console.WriteLine(response.Body);
		}
    }
}
