using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SchoolLib.Data.Validators;

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
        [Required(ErrorMessage = "Необхідно надати ідентифікаційний номер")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None), Column("ReaderId")]
        [Display(Name = "Ідентифікаційний номер")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:D6}")]
        [Range(1, 100000, ErrorMessage = "Ідентифікаційний номер має можливий діапазон від {1} до {2}")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Необхідно надати ім'я")]
        [Display(Name = "Ім'я")]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "Ім'я повинно містити від 2 до 15 символів")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Необхідно надати призвіще")]
        [Display(Name = "Призвіще")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Призвіще повинно містити від 2 до 20 символів")]
        public string SurName { get; set; }

        [Required(ErrorMessage = "Необхідно заповнити поле ім'я по-батькові")]
        [Display(Name = "По-батькові")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "По-батькові повинно містити від 2 до 25 символів")]
        public string Patronimic { get; set; }

        [Required(ErrorMessage = "Необхідно надати назву вулиці")]
        [Display(Name = "Вулиця")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Назва вулиці повинна містити від 3 до 25 символів")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Необхідно надати номер дому")]
        [Display(Name = "Дім")]
        [StringLength(8, MinimumLength = 1, ErrorMessage = "Номер дому повинен містити від 1 до 8 символів")]
        public string House { get; set; }
        
        [Display(Name = "Квартира")]
        [Range(1, 1000, ErrorMessage = "Номер квартири має діапазон значеннь від {1} до {2}")]
        public short Apartment { get; set; }

        [Required(ErrorMessage = "Необхідно надати дату реєстрації")]
        [Display(Name = "Дата реєстрації")]
        [DateRange("-12")]
        public string FirstRegistrationDate { get; set; }

        [Required(ErrorMessage = "Необхідно надати дату перереєстрації")]
        [Display(Name = "Дата перереєстрації")]
        [DateRange("-12")]
        public string LastRegistrationDate { get; set; }
        
        [Display(Name = "Тип")]
        [StringLength(30)]
        public string Discriminator { get; set; }

        [Display(Name = "Примітка")]
        [StringLength(250, ErrorMessage = "Опис читача не може містити більше 250 символів")]
        public string Note { get; set; }

        [Required]
        [Display(Name = "Статус")]
        public ReaderStatus Status { get; set; }

        [Display(Name = "Вибуття")]
        public Drop Drop { get; set; }
    }
}
