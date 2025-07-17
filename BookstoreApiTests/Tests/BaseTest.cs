using System;
using BookstoreApiTests.Clients;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace BookstoreApiTests.Tests
{
    public class BaseTest
    {
        protected BookClient _bookClient;
        protected AuthorClient _authorClient;
        protected IConfigurationRoot Configuration;

        [SetUp]
        public void Setup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();  // env vars override JSON

            Configuration = builder.Build();

            var baseUrl = Configuration["BASE_URL"];
            if (string.IsNullOrEmpty(baseUrl))
            {
                throw new Exception("BaseUrl configuration is missing.");
            }

            _bookClient = new BookClient(baseUrl);
            _authorClient = new AuthorClient(baseUrl);
        }
    }
}
