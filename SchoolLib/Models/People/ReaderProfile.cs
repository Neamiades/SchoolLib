using System.ComponentModel.DataAnnotations;

namespace SchoolLib.Models.People
{
    public class ReaderProfile
    {
        public int Id { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "Ім'я повинно містити від 2 до 15 символів")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Призвіще повинно містити від 2 до 20 символів")]
        public string SurName { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "По-батькові повинно містити від 2 до 25 символів")]
        public string Patronimic { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Назва вулиці повинна містити від 3 до 25 символів")]
        public string Street { get; set; }

        [Required, Range(1, 1000, ErrorMessage = "Номер квартири має діапазон значеннь від {1} до {2}")]
        public short Apartment { get; set; }

        [Required]
        [StringLength(5, MinimumLength = 1, ErrorMessage = "Номер дому повинен містити від 1 до 5 символів")]
        public string House { get; set; }

        public int ReaderId { get; set; }
        public Reader Reader { get; set; }
    }
}
