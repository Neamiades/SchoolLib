using SchoolLib.Data.Validators;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolLib.Models.Books
{
    [DisplayName("Походження")]
    public class Provenance
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id  { get; set; }

        [Display(Name = "Номер накладної")]
        [Required, Range(1, 100000, ErrorMessage = "Номер накладної має діапазон від {1} до {2}")]
        public int WayBill { get; set; }

        [Required(ErrorMessage = "Необхідно встановити дату")]
        [Display(Name = "Дата прийняття")]
        [DateRange("01.01.1990",
            ErrorMessage = "Значення має бути між 01.01.1990 та сьогоднішнім числом у форматі дд.мм.рррр")]
        public string ReceiptDate { get; set; }

        [Required, MinLength(4)]
        [Display(Name = "Місце походження")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Місце походження повинно мати від 4 до 30 символів")]
        public string Place { get; set; }

        [Display(Name = "Примітка")]
        [StringLength(250, ErrorMessage = "Максимальна довжина примітки складає 250 символів")]
        public string Note { get; set; }

        [Display(Name = "Інвентарний номер книги")]
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
