using Microsoft.EntityFrameworkCore;
using Store.Books.Domain;
using Store.Books.Domain.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Product.Api.Data
{
    public class ProductDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Price> Prices { get; set; }

        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }
       
        //Add-Migration
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }

        // Конструктор для разработки, остальные конструкторы отключить
        //Update-Database
        //public ProductDbContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
          => optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=books;Trusted_Connection=True");
    }
}
