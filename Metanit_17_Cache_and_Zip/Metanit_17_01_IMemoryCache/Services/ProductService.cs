using Metanit_17_01_IMemoryCache.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*
    Через встроенный механизм внедрения зависимостей в конструкторе мы можем получить объект
    кэша IMemoryCache. Применяя методы интефейса IMemoryCache, мы можем управлять кэшем:
        bool TryGetValue(object key, out object value): пытаемся получить элемент по ключу key. 
            При успешном получении параметр value заполняется полученным элементом, а метод возвращает true
        object Get(object key): дополнительный метод расширения, который получает по ключу key 
            элемент и возвращает его
        void Remove(object key): удаляет из кэша элемент по ключу key
        object Set(object key, object value, MemoryCacheEntryOptions options): добавляет в кэш элемент
            с ключом key и значением value, применяя опции кэширования MemoryCacheEntryOptions

    По сути встроенная реализация интерфейса IMemoryCache - класс MemoryCache, который используется
    по умолчанию, инкапсулирует все объекты кэша в виде словаря Dictionary.

    Здесь же кэширование реализуется в двух случаях: при получение объкта по id из бд и при добавлении
    этого объекта.
*/

namespace Metanit_17_01_IMemoryCache.Services
{
    public class ProductService
    {
        private MobileContext db;
        private IMemoryCache cache;
        public ProductService(MobileContext context, IMemoryCache memoryCache)
        {
            db = context;
            cache = memoryCache;
        }

        public void Initialize()
        {
            if (!db.Products.Any())
            {
                db.Products.AddRange(
                    new Product { Name = "iPhone 8", Company = "Apple", Price = 600 },
                    new Product { Name = "Galaxy S9", Company = "Samsung", Price = 550 },
                    new Product { Name = "Pixel 2", Company = "Google", Price = 500 }
                );
                db.SaveChanges();
            }
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await db.Products.ToListAsync();
        }

        public void AddProduct(Product product)
        {
            db.Products.Add(product);
            int n = db.SaveChanges();
            if (n > 0)
            {
                cache.Set(product.Id, product, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                });
            }
        }

        public async Task<Product> GetProduct(int id)
        {
            Product product = null;
            if (!cache.TryGetValue(id, out product))
            {
                product = await db.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (product != null)
                {
                    cache.Set(product.Id, product,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                }
            }
            return product;
        }
    }
}
