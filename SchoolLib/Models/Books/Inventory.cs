using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolLib.Models.Books
{
    public class Inventory
    {
        public int Id { get; set; }

        public int ActNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime Year { get; set; }

        [Required, StringLength(50, MinimumLength = 5)]
        public string Couse { get; set; }

        [MaxLength(250)]
        public string Note { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
