using SchoolLib.Models.People;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolLib.Models.Books
{
    public class Issuance
    {
        public int Id { get; set; }

        /*!todo: Исправить диапазон даты*/
        [Required]
        [Range(typeof(DateTime), "6/1/2017", "6/30/2017",
        ErrorMessage = "Значення для дати видачі має бути між {1} та {2}")]
        public DateTime IssueDate { get; set; }

        /*!todo: Исправить диапазон даты*/
        public DateTime AcceptanceDate { get; set; }

        [Required, StringLength(50, MinimumLength = 5, ErrorMessage = "Довжина причини має від 4 до 50 символів")]
        public string Couse { get; set; }

        [StringLength(250, ErrorMessage = "Максимальна довжина примітки складає 250 символів")]
        public string Note { get; set; }

        public int ReaderId { get; set; }
        public Reader Reader { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
