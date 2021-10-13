using Microsoft.EntityFrameworkCore;
using Store.Books.Domain;
using Store.Books.Infrastructure.Data;
using Store.Books.Infrastructure.Interfaces;
using Store.Product.Api.Repos;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Books.Infrastructure.Repos
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(BookDbContext context) : base(context) { }

        public async Task<Author> GetByTitle(string title)
        {
            var newContext = (BookDbContext)context;
            var result = await newContext.Authors
                .Where(p => p.FirstName == title).ToListAsync();

            var result0 = from p in newContext.Authors
            where p.FirstName == title
            select p;

            return result0.FirstOrDefault();
        }
    }
}
