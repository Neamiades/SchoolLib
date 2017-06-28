
using SchoolLib.Models.Books;
using System.Collections.Generic;

namespace SchoolLib.Models.StatisticViewModels
{
    //public class BooksStatisticViewModel
    //{
    //    public int allAdBooks { get; set; }
    //    public int allStBooks { get; set; }

    //    public int allAdBooksOnHadns { get; set; }
    //    public int allStBooksOnHadns { get; set; }

    //    public int allAdBooksInStock { get; set; }
    //    public int allStBooksInStock { get; set; }

    //    public int allIssuedAdBooks { get; set; }
    //    public int allIssuedStBooks { get; set; }

    //    public int allReturnedAdBooks { get; set; }
    //    public int allReturnedStBooks { get; set; }
    //}
    public class BooksStatisticViewModel
    {
        public List<AdditionalBook> AdBooks { get; set; }
        public List<StudyBook> StBooks { get; set; }
    }
}
