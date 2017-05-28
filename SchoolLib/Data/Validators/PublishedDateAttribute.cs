using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolLib.Data.Validators
{
    public class PublishedDateAttribute : ValidationAttribute
    {
        //массив для хранения допустимых имен
        string[] _names;

        public PublishedDateAttribute(string[] names)
        {
            _names = names;
        }
        public override bool IsValid(object value)
        {
            if (_names.Contains(value.ToString()))
                return true;

            return false;
        }
    }
}
