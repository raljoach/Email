using System;
using System.Threading.Tasks;

namespace Email
{
    public interface IEmailClient
    {
        Task Send(EmailData email);
    }
}
