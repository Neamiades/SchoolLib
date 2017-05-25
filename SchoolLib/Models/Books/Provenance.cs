using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolLib.Models.Books
{
    public class Provenance
    {
        public int Id  { get; set; }

        [Required, MinLength(4)]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Місце походження повинно мати від 4 до 30 символів")]
        public string Place { get; set; }

        [Required, Range(1,100000, ErrorMessage = "Номер накладної має діапазон від {1} до {2}")]
        public int WayBill { get; set; }

        /*!todo: Исправить диапазон даты*/
        [Required, Column(TypeName = "date")]
        [Range(typeof(DateTime), "1/1/2004", "6/30/2017",
        ErrorMessage = "Значення для року прийняття має бути між {1} та {2}")]
        public DateTime ReceiptDate { get; set; }

        [StringLength(250, ErrorMessage = "Максимальна довжина примітки складає 250 символів")]
        public string Note { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
