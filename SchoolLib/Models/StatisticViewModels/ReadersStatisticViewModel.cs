
using SchoolLib.Models.People;
using System.Collections.Generic;

namespace SchoolLib.Models.StatisticViewModels
{
    //public class ReadersStatisticViewModel
    //{
    //    public int allStudents { get; set; }
    //    public int allWorkers { get; set; }

    //    public int allEnabledStudents { get; set; }
    //    public int allEnabledWorkers { get; set; }

    //    public int allDisabledStudents { get; set; }
    //    public int allDisabledWorkers { get; set; }

    //    public int allDroppedStudents { get; set; }
    //    public int allDroppedWorkers { get; set; }
    //}
    public class ReadersStatisticViewModel
    {
        public List<Student> Students { get; set; }
        public List<Worker> Workers { get; set; }
    }
}
