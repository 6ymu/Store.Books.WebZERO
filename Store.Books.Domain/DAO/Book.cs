using Store.Books.Domain.Base;

namespace Store.Books.Domain
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public int Year { get; set; }
    }
}
