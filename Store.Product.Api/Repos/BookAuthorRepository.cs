using Microsoft.EntityFrameworkCore;
using Store.Books.Domain;
using Store.Books.Infrastructure.Interfaces;
using Store.Books.Infrastructure;
using Store.Product.Api.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Product.Api.Repos
{
    public class BookAuthorRepository : GenericRepository<BookAuthor>, IBookAuthorRepository
    {
        public BookAuthorRepository(ProductDbContext context) : base(context) { }

        public async Task<BookAuthor> GetByBookId(int id)
        {
            var newContext = (ProductDbContext)context;
            var result = await newContext.BookAuthors
                .Where(p => p.Book.Id == id).ToListAsync();

            var result0 = from p in newContext.BookAuthors
                          where p.Book.Id == id
                          select p;

            return result0.FirstOrDefault();
        }
        public async Task<BookAuthor> GetByAuthorId(int id)
        {
            var newContext = (ProductDbContext)context;
            var result = await newContext.BookAuthors
                .Where(p => p.Author.Id == id).ToListAsync();

            var result0 = from p in newContext.BookAuthors
                          where p.Author.Id == id
                          select p;

            return result0.FirstOrDefault();
        }
    }
}
