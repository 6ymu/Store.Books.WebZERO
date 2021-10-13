using Store.Books.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Books.Domain
{
    public class BookGenre : BaseEntity
    {
        public Book Book { get; set; }
        public Genre Genre { get; set; }
    }
}
