using SchoolLib.Data.Validators;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolLib.Models.People
{
    [Flags]
    [DisplayName("Статус читача")]
    public enum ReaderStatus
    {
        [Display(Name = "Активний")]
        Enabled = 1,
        [Display(Name = "Деактивований")]
        Disabled = 2,
        [Display(Name = "Вибув")]
        Removed = 4,
        [Display(Name = "Всі")]
        All = Enabled | Disabled | Removed
    }

    [Table("Readers")]
    [DisplayName("Читач")]
    abstract public class Reader
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column("ReaderId")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Ідентифікаційний номер")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:D6}")]
        [Range(1, 100000, ErrorMessage = "Ідентифікаційний номер має можливий діапазон від {1} до {2}")]
        public int IdNum { get; set; }

        [Required]
        [Display(Name = "Статус")]
        public ReaderStatus Status { get; set; }

        [Display(Name = "Дата перереєстрації")]
        [Column(TypeName = "date")]
        [DateRange("-12")]
        public string LastRegistrationDate { get; set; }

        [Display(Name = "Дата реєстрації")]
        [Column(TypeName = "date")]
        [DateRange("-12")]
        public string FirstRegistrationDate { get; set; }

        [Display(Name = "Тип")]
        [StringLength(30)]
        public string Discriminator { get; set; }

        public ReaderProfile ReaderProfile { get; set; }
    }
}
