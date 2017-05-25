using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolLib.Models.People
{
    [Flags]
    public enum ReaderStatus
    {
        Enabled = 1,
        Disabled = 2,
        Removed = 4,
        Any = Enabled | Disabled | Removed
    }

    [Table("Readers")]
    public class Reader
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column("ReaderId")]
        public int Id { get; set; }

        [Required]
        public ReaderStatus Status { get; set; }
        
        [Column(TypeName = "date")]
        public DateTime LastRegistrationDate { get; set; }
        
        [Column(TypeName = "date")]
        public DateTime FirstRegistrationDate { get; set; }

        [StringLength(30)]
        public string Discriminator { get; set; }

        public ReaderProfile ReaderProfile { get; set; }
    }
}
