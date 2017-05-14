using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolLib.Models.Books
{
    public class Inventory
    {
        public int Id { get; set; }

        public int ActNumber { get; set; }

        public DateTime Year { get; set; }

        [Required, StringLength(30, MinimumLength = 5)]
        public string Couse { get; set; }

        [MaxLength(250)]
        public string Note { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
