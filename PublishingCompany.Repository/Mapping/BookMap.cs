using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PublishingCompany.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace PublishingCompany.Repository.Mapping
{
    public class BookMap : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Book");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.AuthorId).IsRequired();
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.ISBN).IsRequired();
        }
    }
}
