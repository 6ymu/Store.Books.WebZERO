using Store.Books.Domain;
using Store.Books.Infrastructure;
using Store.Books.Infrastructure.Interfaces;
using Store.Books.Order.Data;

namespace Store.Books.Order.Repos
{
    public class OrderRepository : GenericRepository<Domain.Order>, IOrderRepository
    {
        public OrderRepository(OrderDbContext context) : base(context) { }
    }
}
