using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolLib.Models.People
{
    [Flags]
    [DisplayName("Статус читача")]
    public enum ReaderStatus
    {
        [DisplayName("Активний")]
        Enabled = 1,
        [DisplayName("Деактивований")]
        Disabled = 2,
        [DisplayName("Вибув")]
        Removed = 4,
        [DisplayName("Всі")]
        All = Enabled | Disabled | Removed
    }

    [Table("Readers")]
    [DisplayName("Читач")]
    abstract public class Reader
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column("ReaderId")]
        public int Id { get; set; }

        [Required]
        [DisplayName("Статус")]
        public ReaderStatus Status { get; set; }

        [DisplayName("Дата перереєстрації")]
        [Column(TypeName = "date")]
        public DateTime LastRegistrationDate { get; set; }

        [DisplayName("Дата реєстрації")]
        [Column(TypeName = "date")]
        public DateTime FirstRegistrationDate { get; set; }

        [DisplayName("Тип")]
        [StringLength(30)]
        public string Discriminator { get; set; }

        public ReaderProfile ReaderProfile { get; set; }
    }
}
