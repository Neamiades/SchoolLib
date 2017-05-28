using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolLib.Models.People
{
    [DisplayName("Вибуття")]
    public class Drop
    {
        public int Id { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Дата")]
        public DateTime Date { get; set; }

        [Display(Name = "Причина")]
        [Required, StringLength(30, MinimumLength = 5, ErrorMessage = "Опис причини може містити від 5 до 30 символів")]
        public string Couse { get; set; }

        [Display(Name = "Примітка")]
        [StringLength(250, ErrorMessage = "Опис не може містити більше 250 символів")]
        public string Note { get; set; }

        public int ReaderId { get; set; }
        public Reader Reader { get; set; }
    }
}
