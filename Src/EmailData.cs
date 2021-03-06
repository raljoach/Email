using System;
using System.Collections.Generic;

namespace Email
{
    public class EmailData
    {
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public List<RecipientData> Recipients { get; set; }		
        public string Subject { get; set; }
        public string TextBody { get; set; }
		public string HtmlBody { get; set; }
    }
}
