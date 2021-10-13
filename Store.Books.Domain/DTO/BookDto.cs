using System.ComponentModel.DataAnnotations;

namespace Store.Books.Model.DTO
{
    public class CreateBookDto
    {
        /*
        [Required(ErrorMessage = "Невалидное имя")]
        [Display(Name = "Имя")]
        [StringLength(50, MinimumLength = 3)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Невалидная фамилия")]
        [Display(Name = "Фамилия")]
        [StringLength(50, MinimumLength = 3)]
        public string SurName { get; set; }        
        */
    }

    public class BookDto
    {
        /*

        [Display(Name = "Идентификатор")]
        public int Id { get; set; }
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Display(Name = "Отчество")]
        public string SurName { get; set; }
        */
    }
}
