using System;
using System.Text.Json.Serialization;

namespace PublishingCompany.Domain
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Year { get; set; }
        [JsonIgnore]
        public Author Author { get; set; }
        public Guid AuthorId { get; set; }
    }
}
