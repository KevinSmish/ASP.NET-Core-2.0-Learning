using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdamFreeman_Ch08_SportsStore.Models
{
    public class EFProductRepository : IProductRepository 
    {
        private ApplicationDbContext context;

        public EFProductRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Product> Products => context.Products;
    }
}
