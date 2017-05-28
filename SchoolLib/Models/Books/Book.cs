using SchoolLib.Data.Validators;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
        public int Id { get; set; }

        [Required]
        [Display(Name = "Інвентарний номер")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:D6}")]
        [Range(1, 100000, ErrorMessage = "Інвентарний номер має можливий діапазон від {1} до {2}")]
        public int InventoryNum { get; set; }

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

        /* !todo: Исправить верхнюю грань диапазона на текущий год */
        [Display(Name = "Рік публікації")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy}")]
        [Range(1564, 2017, ErrorMessage = "Значення року видання має бути між {1} та {2}")]
        public short Published { get; set; }

        [Display(Name = "Ціна")]
        [Required(ErrorMessage = "Необхідно встановити ціну")]
        //[UIHint("Decimal")]
        //[DataType(DataType.Currency)]
        [StringLength(6, ErrorMessage = "Ціна не може перевищувати 1000 грн")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Некоректна ціна")]
        //[Range(0.0, 1000.0, ErrorMessage = "Книга може мати вартість від 0 до 1000 грн")]
        public string Price { get; set; }

        //[Remote(action: "CheckEmail", controller: "Book", ErrorMessage = "Email уже используется")]
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
