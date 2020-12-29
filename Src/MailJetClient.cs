using Mailjet.Client;
using Mailjet.Client.Resources;
using System;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Email
{
    public class MailJetClient : IEmailClient
    {
		private string apiKey;
		private string apiSecret;
		public MailJetClient(string apiKey, string apiSecret)
		{
			this.apiKey = apiKey;
			this.apiSecret = apiSecret;
		}
		
		/* Test cases
		   T1: No recipients
		   T2: 1 recipient
		   T3: 2+ recipients
		   T4: Recipient: bad email, blank email, space before after email, space in middle aka ral joach@gmail.com, nonexistent domain, email with dash, email with . email comma, email with 2 @ aka raljoach@@gmail.com
		   T5: From: bad email, blank email, space before after email, space in middle aka ral joach@gmail.com, nonexistent domain, email with dash, email with . email comma, email with 2 @ aka raljoach@@gmail.com
		   T6: From not registered with MailJet => ErrorCode ?
		   T7: 404 Network down => MailJet server unavailable
		   T8: 400 => MailJet returned 400
		   T9: Timeout => Call to MailJet times out
		   T10: Subject: null, empty, blanks, blank before after, numbers, symbols, excessively long, 1 char, unicode, windings, german
		   T11: TextPart: null, empty, blanks, blanks before after, numbers, symbols, excessively long, 1 char, unicode, windings, german
		   T12: HtmlPart: <html> and no end tag, bad html aka <html> <input> </html> , null, empty, blanks, blanks before after, numbers, symbols, excessively long, 1 char, unicode, windings, german
		   T13: Bad apiKey, null, empty string, blanks, blanks before after, too long
		   T14: Bad apiSecret, null, empty string, blanks, blanks before after, too long
		*/
        public async Task SendAsync(EmailData email)
        {
            MailjetClient client = new MailjetClient(apiKey, apiSecret);
			var recipients = new JArray();
			foreach(var r in email.Recipients)
			{
				recipients.Add(
				new JObject {
                 {"Email", r.Email}
                 }
				 );
			}
            MailjetRequest request = new MailjetRequest()
            {
                Resource = Send.Resource,
            }            
           .Property(Send.FromEmail, email.FromEmail)
           .Property(Send.FromName, email.FromName)
           .Property(Send.Subject, email.Subject)
           .Property(Send.TextPart, email.TextBody)
           .Property(Send.HtmlPart, email.HtmlBody)
		   .Property(Send.Recipients,recipients);
           /*.Property(Send.Recipients, new JArray {
                new JObject {
                 {"Email", "raljoach@gmail.com"}
                 }
               });*/
            MailjetResponse response = await client.PostAsync(request);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
                Console.WriteLine(response.GetData());
            }
            else
            {
                Console.WriteLine(string.Format("StatusCode: {0}\n", response.StatusCode));
                Console.WriteLine(string.Format("ErrorInfo: {0}\n", response.GetErrorInfo()));
                Console.WriteLine(string.Format("ErrorMessage: {0}\n", response.GetErrorMessage()));
            }
        }
    }
}
