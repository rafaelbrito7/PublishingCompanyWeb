using System;
using System.Collections.Generic;
using System.Text;

namespace PublishingCompany.Business
{
    interface IAuthorService
    {
        IAuthorDbOperations AuthorDbOperations { get; set; }
        Task<>
    }
}
