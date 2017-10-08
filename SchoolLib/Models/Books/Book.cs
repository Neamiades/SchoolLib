using SchoolLib.Data.Validators;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolLib.Models.Books
{
    [Flags]
    [DisplayName("Статус книги")]
    public enum BookStatus
    {
        [Display(Name = "В бібліотеці")]
        InStock = 1,
        [Display(Name = "У читача")]
        OnHands = 2,
        [Display(Name = "Будь-який")]
        Any = InStock | OnHands
    }
    [DisplayName("Книга")]
    public abstract class Book
    {
        [Required(ErrorMessage = "Необхідно надати інвентарний номер книги")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None), Column("BookId")]
        [Display(Name = "Інвентарний №")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:D6}")]
        [Range(1, 100000, ErrorMessage = "Інвентарний № має можливий діапазон від {1} до {2}")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Необхідно надати назву книги")]
        [Display(Name = "Назва")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "Назва може мати від 2 до 60 символів")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Необхідно надати ім'я автора")]
        [Display(Name = "Автор")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Ім'я автора може мати від 4 до 30 символів")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Необхідно надати авторський шифр")]
        [Display(Name = "Авторський шифр")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Авторський шифр може мати від 3 до 15 символів")]
        public string AuthorCipher { get; set; }

        [Required(ErrorMessage = "Необхідно надати рік публікації книги")]
        [Display(Name = "Рік публікації")]
        [YearRangeValidator(1564)]
        public short Published { get; set; }

        [Display(Name = "Ціна")]
        [Required(ErrorMessage = "Необхідно встановити ціну")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Ціна повинна мати формат Ч[ЧЧ].Ч[Ч] (Ч - число, [] - необов'язковий параметр)")]
        [StringLength(6, ErrorMessage = "Ціна не може перевищувати 1000 грн")] // !todo: Изменить на свой валидатор
        public string Price { get; set; }
        
        [Display(Name = "Примітка")]
        [StringLength(250, ErrorMessage = "Опис книги не може містити більше 250 символів")]
        public string Note { get; set; }

        [Display(Name = "Тип")]
        [StringLength(30)]
        public string Discriminator { get; set; }

        [Required]
        [Display(Name = "Статус")]
        public BookStatus Status { get; set; }

        public Inventory Inventory { get; set; }

        public Provenance Provenance { get; set; }
    }
}
