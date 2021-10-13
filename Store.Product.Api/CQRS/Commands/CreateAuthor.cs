using MediatR;
using Store.Books.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Product.Api.CQRS.Commands
{
    public class CreateAuthor : IRequest<bool>
    {
        public string FirstName { get; set; }
        public string SurName { get; set; }
    }

    public class CreateAuthorCommandHandler: IRequestHandler<CreateAuthor, bool>
    {
        private readonly IAuthorRepository _repository;


        public CreateAuthorCommandHandler(IAuthorRepository repository) 
        {
            _repository = repository;
        }

        public async Task<bool> Handle(CreateAuthor command, CancellationToken cancellationToken)
        {
            await _repository.Insert(new Books.Domain.Author
            {
                FirstName = command.FirstName,
                SurName= command.SurName
            });
            
            return true;
        }
    }


}
