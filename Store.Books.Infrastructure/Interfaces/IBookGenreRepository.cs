using Store.Books.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Store.Books.Infrastructure.Interfaces
{
    public interface IBookGenreRepository : IGenericRepository<BookGenre>
    {
        Task<BookGenre> GetByBookId(int id);
        Task<BookGenre> GetByGenreId(int id);

    }
}
