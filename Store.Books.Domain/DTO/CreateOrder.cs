
namespace Store.Books.Model.DTO
{
    public class CreateOrder
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public int PriceId { get; set; }
        public decimal Amount { get; set; }
        public string UserName { get; set; }
    }

}
