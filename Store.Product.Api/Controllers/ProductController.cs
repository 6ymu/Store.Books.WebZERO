using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.Books.Infrastructure.Interfaces;
using Store.Books.Infrastructure.Repos;
using Store.Product.Api.CQRS.Commands;
using Store.Product.Api.CQRS.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Product.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        
        private readonly IMediator _mediator;
        public ProductController( IMediator mediator,ILogger<ProductController> logger)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string title)
        {
            return Ok(await _mediator.Send(new BooksQuery { Title = title}));
        }
        [HttpGet("byId")]
        public async Task<IActionResult> GetById(int id)
        {
            //return Ok(await _bookRepository.GetById(id));
            return Ok(await _mediator.Send(new BooksQuery { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> Create (CreateBookCommand command, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(command, cancellationToken));
        }



    }
}
