using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SchoolLib.Models.Books
{
    [Flags]
    [DisplayName("Статус книги")]
    public enum BookStatus
    {
        [DisplayName("В бібліотеці")]
        InStock = 1,
        [DisplayName("У читача")]
        OnHands = 2,
        [DisplayName("Всі")]
        All = InStock | OnHands
    }
    public abstract class Book
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Інвентарний номер")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Інвентарний номер повинен мати 6 цифер")]
        public string InventoryNum { get; set; }

        [Required]
        [DisplayName("Назва")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "Назва може мати від 2 до 60 символів")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Автор")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Ім'я автора може мати від 4 до 30 символів")]
        public string Author { get; set; }

        [Required]
        [DisplayName("Авторський шифр")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Авторський шифр може мати від 3 до 15 символів")]
        public string AuthorCipher { get; set; }

        [DisplayName("Рік публікації")]
        /* !todo: Исправить верхнюю грань диапазона на текущий год */
        [Required, Range(1564, 2017, ErrorMessage = "Рік видання має можливий діапазон від {1} до {2}")]
        public short Published { get; set; }

        [DisplayName("Ціна")]
        [Required, Range(0, 1000, ErrorMessage = "Книга може мати вартість від 0 до 1000 грн")]
        public decimal Price { get; set; }

        [Required]
        [DisplayName("Примітка")]
        [StringLength(250, ErrorMessage = "Опис книги не може містити більше 250 символів")]
        public string Note { get; set; }

        [DisplayName("Тип")]
        [StringLength(30)]
        public string Discriminator { get; set; }

        [Required]
        [DisplayName("Статус")]
        public BookStatus Status { get; set; }

        public Inventory Inventory { get; set; }

        public Provenance Provenance { get; set; }
    }
}
