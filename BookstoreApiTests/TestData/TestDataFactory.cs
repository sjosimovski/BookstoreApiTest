using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookstoreApiTests.Models;

namespace BookstoreApiTests.TestData
{
    public static class TestDataFactory
    {
        private static readonly Random _rand = new();

        public static Book GenerateBook(
        int? id = null,
        string title = null,
        int? pageCount = null,
        DateTime? publishDate = null)
        {
            return new Book
            {
                Id = id ?? _rand.Next(10000, 99999),
                Title = title ?? "Book " + Guid.NewGuid().ToString().Substring(0, 5),
                PageCount = pageCount ?? _rand.Next(50, 1000),
                PublishDate = publishDate ?? DateTime.UtcNow.AddDays(-_rand.Next(0, 365))
            };
        }


        public static Author GenerateAuthor(
        string firstName = null,
        string lastName = null,
        int? id = null)
        {
            return new Author
            {
                Id = id ?? _rand.Next(10000, 99999),
                FirstName = firstName ?? "Author" + Guid.NewGuid().ToString().Substring(0, 5),
                LastName = lastName ?? "Lastname" + Guid.NewGuid().ToString().Substring(0, 5)
            };
        }
    }
}
