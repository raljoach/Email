using System;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Email;


namespace Tests.Email
{
    public class IntegrationTestEmailController
    {
		private readonly WebApplicationFactory<Startup> factory = new WebApplicationFactory<Startup>();


        [Fact]
        public async void StatusTest()
        {
            // arrange
            var client = factory.CreateClient();

            // act
            var response = await client.GetAsync("/email/status");            
            

            // Do the verifications
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            var body = await response.Content.ReadAsStringAsync();
            Assert.Equal("Service is available", body);
        }
    }
}
