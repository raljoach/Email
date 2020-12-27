using System;

namespace Email
{
    public interface IEmailClient
    {
        async Task Send(EmailData email);
    }
}
