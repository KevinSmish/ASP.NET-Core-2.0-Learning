using System.Linq;
using AdamFreeman_Ch08_SportsStore.Models;
using AdamFreeman_Ch08_SportsStore.Models.ViewModels;

namespace AdamFreeman_Ch08_SportsStore.Controllers
{
    internal class ProductsListViewModel
    {
        public IQueryable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}