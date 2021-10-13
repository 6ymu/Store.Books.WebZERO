using Microsoft.EntityFrameworkCore;
using Store.Books.Domain;
using Store.Books.Infrastructure.Interfaces;
using Store.Books.Infrastructure;
using Store.Product.Api.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Product.Api.Repos
{
    public class PriceRepository : GenericRepository<Price>, IPriceRepository
    {
        public PriceRepository(ProductDbContext context) : base(context) { }

        public async Task<Price> GetByAmount(decimal amount)
        {
            var newContext = (ProductDbContext)context;
            var result = await newContext.Prices
                .Where(p => p.Amount == amount).ToListAsync();

            var result0 = from p in newContext.Prices
                          where p.Amount == amount
                          select p;

            return result0.FirstOrDefault();
        }
    }
}
