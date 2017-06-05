using SchoolLib.Data.Validators;
using SchoolLib.Models.People;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolLib.Models.Books
{
    [DisplayName("Видача")]
    public class Issuance
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Необхідно заповнити дату видачі")]
        [Display(Name = "Дата видачі")]
        [DateRange("-6")]
        public string IssueDate { get; set; }
        
        [Display(Name = "Дата прийняття")]
        [DateRange("-6")]
        public string AcceptanceDate { get; set; }
        
        [Display(Name = "Причина")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Довжина причини має від 4 до 50 символів")]
        public string Couse { get; set; }

        [Display(Name = "Примітка")]
        [StringLength(250, ErrorMessage = "Максимальна довжина примітки складає 250 символів")]
        public string Note { get; set; }

        [Display(Name = "Ідентифікаційний номер")]
        public int ReaderId { get; set; }
        public Reader Reader { get; set; }

        [Display(Name = "Інвентарний номер")]
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
