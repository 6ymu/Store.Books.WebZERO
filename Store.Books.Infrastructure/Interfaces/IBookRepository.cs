using Store.Books.Domain;
using System.Threading.Tasks;

namespace Store.Books.Infrastructure.Interfaces
{
    public interface IBookRepository : IGenericRepository<Book> 
    {
        Task<Book> GetByTitle(string title);
    }
}
