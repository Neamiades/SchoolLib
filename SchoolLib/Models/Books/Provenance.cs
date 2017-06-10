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

        [Required(ErrorMessage = "Необхідно надати номер накладної")]
        [Display(Name = "Номер накладної")]
        [Range(1, 100000, ErrorMessage = "Номер накладної має діапазон від {1} до {2}")]
        public int WayBill { get; set; }

        [Required(ErrorMessage = "Необхідно встановити дату прийняти")]
        [Display(Name = "Дата прийняття")]
        [StringLength(10, ErrorMessage = "Некоректний формат дати")]
        [DateRange("-5")]
        public string ReceiptDate { get; set; }

        [Required(ErrorMessage = "Необхідно надати назву місця походження")]
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
