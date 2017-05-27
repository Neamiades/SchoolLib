using SchoolLib.Models.People;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SchoolLib.Models.Books
{
    [DisplayName("Видача")]
    public class Issuance
    {
        public int Id { get; set; }

        /*!todo: Исправить диапазон даты*/
        [Required]
        [DisplayName("Дата видачі")]
        [Range(typeof(DateTime), "6/1/2017", "6/30/2017",
        ErrorMessage = "Значення для дати видачі має бути між {1} та {2}")]
        public DateTime IssueDate { get; set; }

        /*!todo: Исправить диапазон даты*/
        [DisplayName("Дата прийняття")]
        public DateTime AcceptanceDate { get; set; }

        [DisplayName("Причина")]
        [Required, StringLength(50, MinimumLength = 5, ErrorMessage = "Довжина причини має від 4 до 50 символів")]
        public string Couse { get; set; }

        [DisplayName("Примітка")]
        [StringLength(250, ErrorMessage = "Максимальна довжина примітки складає 250 символів")]
        public string Note { get; set; }

        public int ReaderId { get; set; }
        public Reader Reader { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
