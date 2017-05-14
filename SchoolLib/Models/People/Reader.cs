using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolLib.Models.People
{
    public enum ReaderType { Worker, Student }
    public enum ReaderStatus { Enabled, Disabled, Removed }

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

        public ReaderProfile ReaderProfile { get; set; }
    }
}
