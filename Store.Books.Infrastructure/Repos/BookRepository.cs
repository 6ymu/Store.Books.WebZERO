using Microsoft.EntityFrameworkCore;
using Store.Books.Domain;
using Store.Books.Infrastructure.Data;
using Store.Books.Infrastructure.Interfaces;
using Store.Product.Api.Repos;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Books.Infrastructure.Repos
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(BookDbContext context) : base(context) { }

        public async Task<Book> GetByTitle(string title)
        {
            var newContext = (BookDbContext)context;
            var result = await newContext.Books
                .Where(p => p.Title == title).ToListAsync();

            var result0 = from p in newContext.Books
            where p.Title == title
            select p;

            return result0.FirstOrDefault();
        }
    }
}
