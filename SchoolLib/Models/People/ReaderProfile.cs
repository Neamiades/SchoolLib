using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SchoolLib.Models.People
{
    [DisplayName("Дані читача")]
    public class ReaderProfile
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Ім'я")]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "Ім'я повинно містити від 2 до 15 символів")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Призвіще")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Призвіще повинно містити від 2 до 20 символів")]
        public string SurName { get; set; }

        [Required]
        [DisplayName("По-батькові")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "По-батькові повинно містити від 2 до 25 символів")]
        public string Patronimic { get; set; }

        [Required]
        [DisplayName("Вулиця")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Назва вулиці повинна містити від 3 до 25 символів")]
        public string Street { get; set; }

        [Required]
        [DisplayName("Дім")]
        [StringLength(5, MinimumLength = 1, ErrorMessage = "Номер дому повинен містити від 1 до 5 символів")]
        public string House { get; set; }

        [DisplayName("Квартира")]
        [Required, Range(1, 1000, ErrorMessage = "Номер квартири має діапазон значеннь від {1} до {2}")]
        public short Apartment { get; set; }

        public int ReaderId { get; set; }
        public Reader Reader { get; set; }
    }
}
