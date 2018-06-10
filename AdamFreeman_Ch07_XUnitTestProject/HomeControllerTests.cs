using AdamFreeman_Ch06_Working_with_VisualStudio.Controllers;
using AdamFreeman_Ch06_Working_with_VisualStudio.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;


namespace AdamFreeman_Ch07_XUnitTestProject
{
    public class HomeControllerTests
    {
        class ModelCompleteFakeRepository : IRepository
        {
            public IEnumerable<Product> Products { get; set; }
            public void AddProduct(Product p)
            {
                // do nothing - not required for test
            }
        }

        [Theory]
        /*
        [InlineData(275, 48.95, 19.50, 24.95)]
        [InlineData(5, 48.95, 19.50, 24.95)]

        public void IndexActionModelIsComplete(decimal price1, decimal price2,
            decimal price3, decimal price4)
        {
            // Arrange
            var controller = new HomeController();
            controller.Repository = new ModelCompleteFakeRepository
            {
                Products = new Product[] {
                        new Product {Name = "P1", Price = price1 },
                        new Product {Name = "P2", Price = price2 },
                        new Product {Name = "P3", Price = price3 },
                        new Product {Name = "P4", Price = price4 },
                }
            };
            */

        [ClassData(typeof(ProductTestData))]
        public void IndexActionModelIsComplete(Product[] products)
        {
            // Arrange
            var controller = new HomeController();
            controller.Repository = new ModelCompleteFakeRepository
            {
                Products = products
            };

            // Act
            var model = (controller.Index() as ViewResult)?.ViewData.Model
                as IEnumerable<Product>;
    
            // Assert
            Assert.Equal(controller.Repository.Products, model,
                Comparer.Get<Product>((p1, p2) => p1.Name == p2.Name
                && p1.Price == p2.Price));
        }

        // --------------------------------------------------------------------
        class PropertyOnceFakeRepository : IRepository
        {
            public int PropertyCounter { get; set; } = 0;
            public IEnumerable<Product> Products
            {
                get
                {
                    PropertyCounter++;
                    return new[] { new Product { Name = "P1", Price = 100 } };
                }
            }
            public void AddProduct(Product p)
            {
                // do nothing - not required for test
            }
        }
        [Fact]
        public void RepositoryPropertyCalledOnce()
        {
            // Arrange
            var repo = new PropertyOnceFakeRepository();
            var controller = new HomeController { Repository = repo };
            // Act
            var result = controller.Index();
            // Assert
            Assert.Equal(1, repo.PropertyCounter);
        }
    }
}
