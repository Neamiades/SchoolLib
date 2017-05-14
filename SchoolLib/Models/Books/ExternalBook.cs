using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolLib.Models.Books
{
    public class ExternalBook : Book
    {
        [Required, StringLength(20, MinimumLength = 5)]
        public string Genre { get; set; }
    }
}
