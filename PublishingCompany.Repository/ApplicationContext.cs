using Microsoft.EntityFrameworkCore;
using PublishingCompany.Domain;
using PublishingCompany.Repository.Mapping;


namespace PublishingCompany.Repository
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public ApplicationContext()
        {

        }

        public DbSet<User> UserDbSet { get; set; }
        public DbSet<Book> BookDbSet { get; set; }
        public DbSet<Author> AuthorDbSet { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new AuthorMap());
            modelBuilder.ApplyConfiguration(new BookMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
