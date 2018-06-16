using AdamFreeman_Ch08_SportsStore.Models;
using AdamFreeman_Ch08_SportsStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdamFreeman_Ch08_SportsStore.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int PageSize = 4;

        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }

        //public ViewResult List() => View(repository.Products);
        // http://localhost:62638/?productPage=2

        public ViewResult List(int productPage = 1)
            => View(new ProductsListViewModel
            {
                Products = repository.Products
                .OrderBy(p => p.ProductID)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize),
            PagingInfo = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItems = repository.Products.Count()
            }
        });
    }
}

