using System;

namespace Email
{
    public class EmailData
    {
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public List<string> Recipients { get; set; }		
        public string Subject { get; set; }
        public string TextBody { get; set; }
		public string HtmlBody { get; set; }
    }
}
