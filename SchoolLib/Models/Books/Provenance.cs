using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolLib.Models.Books
{
    public class Provenance
    {
        public int Id  { get; set; }

        [Required, MinLength(6), MaxLength(30)]
        public string Place { get; set; }

        public int WayBill { get; set; }
        
        public DateTime ReceiptDate { get; set; }

        [MaxLength(250)]
        public string Note { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
