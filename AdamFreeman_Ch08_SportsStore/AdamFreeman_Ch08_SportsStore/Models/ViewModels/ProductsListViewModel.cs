using System.Collections.Generic;
using AdamFreeman_Ch08_SportsStore.Models;

namespace AdamFreeman_Ch08_SportsStore.Models.ViewModels
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
