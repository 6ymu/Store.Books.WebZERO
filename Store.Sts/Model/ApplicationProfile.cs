using System;
using System.ComponentModel.DataAnnotations;

namespace Store.Sts.Model
{
    public class ApplicationProfile 
    {
        [Key]
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }

}
