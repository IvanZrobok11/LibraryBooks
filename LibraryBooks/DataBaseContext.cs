using System.Linq;
using LibraryBooks.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryBooks
{
    public class DataBaseContext : DbContext
    {
        public const string DatabaseName = "libraryBooks";
        private string _dbPassword = "sa";
        private string _dbUserName = "sa";
        public DataBaseContext()
        {
            Database.EnsureCreated();
        }
        public DbSet<Book> Books { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql($"host=localhost; port=5432; database={DatabaseName}; username={_dbUserName}; password={_dbPassword}; ");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.Entity<Book>();
           // modelBuilder.Entity<Book>().MapToStoredProcedures
        }

        
    }
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(u => new {u.Id, u.BarCode});
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.BarCode).HasColumnName("bar_code");
            builder.Property(x => x.Author).HasColumnName("author");
            builder.Property(x => x.Description).HasColumnName("description");
            builder.Property(x => x.Name).HasColumnName("name");
            builder.Property(x => x.Price).HasColumnName("price");
            builder.Property(x => x.Quantity).HasColumnName("quantity");
            builder.ToTable("books");
        }
    }
}
