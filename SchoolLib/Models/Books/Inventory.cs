using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SchoolLib.Data.Validators;

namespace SchoolLib.Models.Books
{
    [DisplayName("Інвентаризація")]
    public class Inventory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Необхідно надати номер акту")]
        [Display(Name = "Номер акту")]
        [Range(1, 100000, ErrorMessage = "Номер акту має діапазон від {1} до {2}")]
        public int ActNumber { get; set; }

        [Required(ErrorMessage = "Необхідно надати рік інвентаризації")]
        [Display(Name = "Рік")]
        [YearRangeValidator(2012)]
        public short Year { get; set; }

        [Display(Name = "Причина")]
        [StringLength(50, MinimumLength = 4)]
        public string Couse { get; set; }

        [Display(Name = "Примітка")]
        [StringLength(250, ErrorMessage = "Максимальна довжина примітки складає 250 символів")]
        public string Note { get; set; }

        [Display(Name = "Інвентарний № книги")]
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
