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
           T3: Shuffle of email clients used => repeatedly create mocks and invoke, until 2nd client is invoked
        */


        [Fact]
        public void SendFailoverTest()
        {
            // Arrange
            var mockFailedClient = new Mock<IEmailClient>();
            var mockEmailClient = new Mock<IEmailClient>();
            var emailClients = new List<IEmailClient>()
            {
                mockFailedClient.Object,
                mockEmailClient.Object
            };

            mockFailedClient.Setup(x=>x.SendAsync(It.IsAny<EmailData>()))
            .ThrowsAsync(new Exception());

            mockEmailClient.Setup(x=>x.SendAsync(It.IsAny<EmailData>()))
            .Returns(Task.CompletedTask);

            // Act
            var emailClient = new EmailService(emailClients);
            emailClient.Send(new EmailData());

            // Assert
            VerifyEmailSent(mockEmailClient);

        }

        [Fact]
        public void SendShuffleTest()
        {
            
            // Arrange
                var mockEmailClient = new Mock<IEmailClient>();
                var mockEmailClient2 = new Mock<IEmailClient>();
                var emailClients = new List<IEmailClient>()
                {
                    mockEmailClient.Object,
                    mockEmailClient2.Object
                };

                mockEmailClient.Setup(x=>x.SendAsync(It.IsAny<EmailData>()))
                .Returns(Task.CompletedTask);

                mockEmailClient2.Setup(x=>x.SendAsync(It.IsAny<EmailData>()))
                .Returns(Task.CompletedTask);

                

                // Act
                var emailClient = new EmailService(emailClients);

                var maxTry = 4;
                var attempt = 0;

                var clientInvoked =0;
                var client2Invoked = 0;
            while(++attempt<=maxTry)
            {
                
                emailClient.Send(new EmailData());

                // Assert
                clientInvoked+=CheckEmailSent(mockEmailClient)?1:0;
                client2Invoked+=CheckEmailSent(mockEmailClient2)?1:0;                
            }

            Assert.Equal(2, clientInvoked);
            Assert.Equal(2, client2Invoked);
        }


        private bool CheckEmailSent(Mock<IEmailClient> mockEmailClient)
        {
            foreach(var inv in mockEmailClient.Invocations)
            {
              if(mockEmailClient == inv.MatchingSetup.Mock) return true;
            }
            return false;
        }
    }
}