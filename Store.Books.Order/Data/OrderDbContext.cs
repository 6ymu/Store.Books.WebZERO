using Microsoft.EntityFrameworkCore;
using Store.Books.Domain;


namespace Store.Books.Order.Data
{
    public class OrderDbContext : DbContext
    {
        public DbSet<Busket> Buskets { get; set; }
        public DbSet<Domain.Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Book> Books { get; set; }
        
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }

        // Конструктор для разработки, остальные конструкторы отключить
        //public OrderDbContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
          => optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=orders;Trusted_Connection=True;");
    }
}
