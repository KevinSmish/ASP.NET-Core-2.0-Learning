using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Metanit_14_01_Validation_Sample.Models
{
    public class PersonNameAttribute : ValidationAttribute
    {
        //массив для хранения допустимых имен
        string[] _names;

        public PersonNameAttribute(string[] names)
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
