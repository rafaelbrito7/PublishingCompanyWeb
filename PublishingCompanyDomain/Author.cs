using System;
using System.Collections.Generic;

namespace PublishingCompany.Domain
{
    
    public class Author
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public virtual List<Book> Books { get; set; }
    }
}
