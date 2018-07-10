using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metanit_15_07_EntityFrameworkPager.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<User> Users { get; set; }
        public Company()
        {
            Users = new List<User>();
        }
    }
}
