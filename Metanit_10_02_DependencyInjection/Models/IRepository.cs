using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metanit_10_02_DependencyInjection.Models
{
    public interface IRepository
    {
        List<Phone> GetPhones();
    }
}
