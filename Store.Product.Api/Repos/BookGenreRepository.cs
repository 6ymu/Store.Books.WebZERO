using Microsoft.EntityFrameworkCore;
using Store.Books.Domain;
using Store.Books.Infrastructure.Interfaces;
using Store.Books.Infrastructure;
using Store.Product.Api.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Product.Api.Repos
{
    public class BookGenreRepository : GenericRepository<BookGenre>, IBookGenreRepository
    {
        public BookGenreRepository(ProductDbContext context) : base(context) { }

        public async Task<BookGenre> GetByBookId(int id)
        {
            var newContext = (ProductDbContext)context;
            var result = await newContext.BookGenres
                .Where(p => p.Book.Id == id).ToListAsync();

            var result0 = from p in newContext.BookGenres
                          where p.Book.Id == id
                          select p;

            return result0.FirstOrDefault();
        }
        public async Task<BookGenre> GetByGenreId(int id)
        {
            var newContext = (ProductDbContext)context;
            var result = await newContext.BookGenres
                .Where(p => p.Genre.Id == id).ToListAsync();

            var result0 = from p in newContext.BookGenres
                          where p.Genre.Id == id
                          select p;

            return result0.FirstOrDefault();
        }
    }
}
