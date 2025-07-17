using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookstoreApiTests.Clients;
using NUnit.Framework;

namespace BookstoreApiTests.Tests;

public class BaseTest
{
    protected BookClient _bookClient;
    protected AuthorClient _authorClient;

    [SetUp]
    public void Setup()
    {
        string baseUrl = "https://fakerestapi.azurewebsites.net/api/v1/";
        _bookClient = new BookClient(baseUrl);
        _authorClient = new AuthorClient(baseUrl);
    }
}

