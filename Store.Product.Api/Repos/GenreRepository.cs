using Microsoft.EntityFrameworkCore;
using Store.Books.Domain;
using Store.Books.Infrastructure.Interfaces;
using Store.Books.Infrastructure;
using Store.Product.Api.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Product.Api.Repos
{
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        public GenreRepository(ProductDbContext context) : base(context) { }

        public async Task<Genre> GetByTitle(string title)
        {
            var newContext = (ProductDbContext)context;
            var result = await newContext.Genres
                .Where(p => p.Title == title).ToListAsync();

            var result0 = from p in newContext.Genres
                          where p.Title == title
                          select p;

            return result0.FirstOrDefault();
        }
    }
}
