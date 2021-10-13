using MediatR;
using Store.Books.Domain;
using Store.Books.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Product.Api.CQRS.Queries
{
    public class BooksQuery : IRequest<IEnumerable<Book>>
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

    public class BooksQueryHandler : IRequestHandler<BooksQuery, IEnumerable<Book>>
    {
        private readonly IBookRepository _repository;

        public BooksQueryHandler(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Book>> Handle(BooksQuery command, CancellationToken cancellationToken)
        {
            return await _repository.Get(p=>p.Title==command.Title);
        }




    }


}
