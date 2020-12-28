using System;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;


namespace Tests.Email
{
    public class IntegrationTestEmailController
    {
		private readonly WebApplicationFactory<Startup> factory = new WebApplicationFactory<Startup>();


        [Fact]
        public void StatusTest()
        {
            var client = factory.CreateClient();

            var response = await client.GetAsync("/status");

            // Do the verifications
            Assert.Equal("Service is available", response)
        }
    }
}
