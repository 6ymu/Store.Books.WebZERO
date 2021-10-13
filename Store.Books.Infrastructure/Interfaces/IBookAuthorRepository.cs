using Store.Books.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Store.Books.Infrastructure.Interfaces
{
    public interface IBookAuthorRepository : IGenericRepository<BookAuthor>
    {
        Task<BookAuthor> GetByBookId(int id);
        Task<BookAuthor> GetByAuthorId(int id);

    }
}
