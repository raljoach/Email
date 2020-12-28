using System;
using System.Threading.Tasks;

namespace Email
{
    public interface IEmailClient
    {
        Task SendAsync(EmailData email);
    }
}
