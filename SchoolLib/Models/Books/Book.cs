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
        [Display(Name = "Всі")]
        All = InStock | OnHands
    }
    [DisplayName("Книжка")]
    public abstract class Book
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None), Column("BookId")]
        [Display(Name = "Інвентарний номер")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:D6}")]
        [Range(1, 100000, ErrorMessage = "Інвентарний номер має можливий діапазон від {1} до {2}")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Назва")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "Назва може мати від 2 до 60 символів")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Автор")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Ім'я автора може мати від 4 до 30 символів")]
        public string Author { get; set; }

        [Required]
        [Display(Name = "Авторський шифр")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Авторський шифр може мати від 3 до 15 символів")]
        public string AuthorCipher { get; set; }
        
        [Required]
        [Display(Name = "Рік публікації")]
        [YearRangeValidator(1564)]
        public short Published { get; set; }

        [Display(Name = "Ціна")]
        [Required(ErrorMessage = "Необхідно встановити ціну")]
        //[UIHint("Decimal")]
        //[DataType(DataType.Currency)]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Некоректна ціна")]
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
