using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metanit_12_04_StrongTypeHtmlHelper.Models
{
    public class Phone
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public bool HasTwoSim { get; set; }
        public string SecretCode { get; set; }
    }
}
