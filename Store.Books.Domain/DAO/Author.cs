using Store.Books.Domain.Base;

namespace Store.Books.Domain
{
    public class Author : BaseEntity
    {
        public string FirstName { get; set; }
        public string SurName { get; set; }
    }
}
