using SchoolLib.Data.Validators;
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
        [Display(Name = "Дата видачі")]
        [DateRange("-5",
            ErrorMessage = "Значення для дати видачі має бути між 01.01.1990 та сьогоднішнім числом у форматі дд.мм.рррр")]
        public string IssueDate { get; set; }

        /*!todo: Исправить диапазон даты*/
        [Display(Name = "Дата прийняття")]
        [DateRange("-5",
            ErrorMessage = "Значення для дати видачі має бути між 01.01.1990 та сьогоднішнім числом у форматі дд.мм.рррр")]
        public string AcceptanceDate { get; set; }

        [Display(Name = "Причина")]
        [Required, StringLength(50, MinimumLength = 5, ErrorMessage = "Довжина причини має від 4 до 50 символів")]
        public string Couse { get; set; }

        [Display(Name = "Примітка")]
        [StringLength(250, ErrorMessage = "Максимальна довжина примітки складає 250 символів")]
        public string Note { get; set; }

        public int ReaderId { get; set; }
        public Reader Reader { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
