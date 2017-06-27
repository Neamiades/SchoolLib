using SchoolLib.Data.Validators;
using SchoolLib.Models.Books;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolLib.Models.People
{
    [DisplayName("Обіг книги")]
    public class Issuance
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Необхідно заповнити дату видачі")]
        [Display(Name = "Дата видачі")]
        [StringLength(10, ErrorMessage = "Некоректний формат дати")]
        [DateRange("-6")]
        public string IssueDate { get; set; }
        
        [Display(Name = "Дата прийняття")]
        [StringLength(10, ErrorMessage = "Некоректний формат дати")]
        [DateRange("-6")]
        public string AcceptanceDate { get; set; }
        
        [Display(Name = "Причина")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Довжина причини має від 4 до 50 символів")]
        public string Couse { get; set; }

        [Display(Name = "Примітка")]
        [StringLength(250, ErrorMessage = "Максимальна довжина примітки складає 250 символів")]
        public string Note { get; set; }

        [Required(ErrorMessage = "Необхідно залишити підпис читача")]
        [Display(Name = "Підпис читача")]
        [StringLength(10, ErrorMessage = "Максимальна довжина примітки складає 10 символів")]
        public string ReaderSign { get; set; }

        [Display(Name = "Підпис бібліотекара")]
        [StringLength(10, ErrorMessage = "Максимальна довжина примітки складає 10 символів")]
        public string UserSign { get; set; }

        [Display(Name = "Ідентифікаційний №")]
        public int ReaderId { get; set; }
        public Reader Reader { get; set; }

        [Display(Name = "Інвентарний №")]
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
