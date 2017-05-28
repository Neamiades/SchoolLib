using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolLib.Models.Books
{
    [DisplayName("Інвентаризація")]
    public class Inventory
    {
        public int Id { get; set; }

        [Display(Name = "Номер акту")]
        [Required, Range(1, 100000, ErrorMessage = "Номер акту має діапазон від {1} до {2}")]
        public int ActNumber { get; set; }

        /*!todo: Исправить диапазон даты*/
        [Display(Name = "Рік")]
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy}")]
        //[Range(typeof(DateTime), "1/1/2004", "30/6/2017",
        //ErrorMessage = "Значення для року інвентаризації має бути між {1} та {2}")]
        public DateTime Year { get; set; }

        [Display(Name = "Причина")]
        [StringLength(50, MinimumLength = 4)]
        public string Couse { get; set; }

        [Display(Name = "Примітка")]
        [MaxLength(250, ErrorMessage = "Максимальна довжина примітки складає 250 символів")]
        public string Note { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
