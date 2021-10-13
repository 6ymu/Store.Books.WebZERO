using Store.Books.Domain;
using Store.Books.Infrastructure;
using Store.Books.Infrastructure.Interfaces;
using Store.Books.Order.Data;

namespace Store.Books.Order.Repos
{
    public class BusketRepository : GenericRepository<Busket>, IBusketRepository
    {
        public BusketRepository(OrderDbContext context) : base(context) { }
    }
}
