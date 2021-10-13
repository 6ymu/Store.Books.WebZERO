using Store.Books.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Store.Books.Infrastructure.Interfaces
{
    public interface IPriceRepository : IGenericRepository<Price>
    {
        Task<Price> GetByAmount(decimal amount);
    }
}
