using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Email
{
    public class EmailService
    {
        private List<IEmailClient> emailClients;

        public EmailService(List<IEmailClient> emailClients)
        {
            this.emailClients = emailClients;
        }

        public async void Send(EmailData emailData)
        {/*
            foreach(var client in emailClients)
            {
                //System.Threading.Tasks.Task.FromException()
                client.SendAsync(email).ContinueWith(ct => ct.)
                System.Threading.Tasks.Task.Wa
            }
*/
            var n = emailClients.Count;
            Task prevTask = null;            
            for(int i=0; i<n; i++)
            {
                var thisClient = emailClients[i];                
                var thisTask = Task.Run(()=>thisClient.SendAsync(emailData));
                if(prevTask!=null)
                {
                    await prevTask.ContinueWith(
                        (ct)=>
                        {
                            if(ct.Exception!=null)
                            {
                                thisClient.SendAsync(emailData);
                            }
                        }
                    );
                }                
                prevTask = thisTask;
            }
        }


    }
}
