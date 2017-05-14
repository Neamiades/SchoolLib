using SchoolLib.Models.People;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolLib.Models.Books
{
    public class Issuance
    {
        public int Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime IssueDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime AcceptanceDate { get; set; }

        [Required, StringLength(30,MinimumLength = 5)]
        public string Couse { get; set; }

        [StringLength(250)]
        public string Note { get; set; }

        public int ReaderId { get; set; }
        public Reader Reader { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
