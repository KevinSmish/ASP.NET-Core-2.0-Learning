using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdamFreeman_Ch08_SportsStore.Models
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
    }
}
