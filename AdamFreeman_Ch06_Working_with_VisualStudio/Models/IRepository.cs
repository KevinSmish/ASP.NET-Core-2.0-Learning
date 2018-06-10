using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdamFreeman_Ch06_Working_with_VisualStudio.Models
{
    public interface IRepository
    {
        IEnumerable<Product> Products { get; }
        void AddProduct(Product p);
    }
}
