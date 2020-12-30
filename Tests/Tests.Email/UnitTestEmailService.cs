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
        }
    }
}