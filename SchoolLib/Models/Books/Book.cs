using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolLib.Models.Books
{
    public enum BookStatus
    {
        InStock,
        OnHands
    }

    public abstract class Book
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2), MaxLength(20, ErrorMessage = "Название должно содержать меньше 20 символов")]
        public string Name { get; set; }

        [Required]
        [MinLength(4), MaxLength(25, ErrorMessage = "Имя автора должно содержать меньше 25 символов")]
        public string Author { get; set; }

        [Required]
        [MinLength(3), MaxLength(15, ErrorMessage = "Шифр автора должен содержать меньше 15 символов")]
        public string AuthorCipher { get; set; }

        [Column(TypeName = "date")]
        public DateTime Published { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(250, ErrorMessage = "Описание книги не может содержать больше 250 символов")]
        public string Note { get; set; }

        [StringLength(30)]
        public string Discriminator { get; set; }

        public BookStatus Status { get; set; }

        public Inventory Inventory { get; set; }

        public Provenance Provenance { get; set; }
    }
}
