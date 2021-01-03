using System;
using System.Collections.Generic;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Email;
using Moq;
using System.Threading.Tasks;


namespace Tests.Email
{
    public class UnitTestEmailService
    {
        [Fact]
        public void SendTest()
        {
            // Arrange
            var mockEmailClient = new Mock<IEmailClient>();
            var emailClients = new List<IEmailClient>()
            {
                  mockEmailClient.Object
            };
            mockEmailClient.Setup(x=>x.SendAsync(It.IsAny<EmailData>()))
            .Returns(Task.CompletedTask);

            // Act
            var emailClient = new EmailService(emailClients);
            emailClient.Send(new EmailData());

            // Assert
            VerifyEmailSent(mockEmailClient);
        }

        private void VerifyEmailSent(Mock<IEmailClient> mockEmailClient)
        {
            mockEmailClient.Verify(mock => mock.SendAsync(It.IsAny<EmailData>()), Times.Once);
        }

        /* Test cases
           T1: Send invokes IEmailClient SendAsync
           T2: Send fails on first client, invokes SendAsync on next IEmailClient
        */
    }
}