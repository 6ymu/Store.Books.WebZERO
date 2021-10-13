using Store.Books.Domain;
using Store.Books.Infrastructure;
using Store.Books.Infrastructure.Data;
using Store.Books.Infrastructure.Interfaces;

namespace Store.Product.Api.Repos
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(BookDbContext context) : base(context) { }
    }
}
