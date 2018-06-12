using AdamFreeman_Ch06_Working_with_VisualStudio.Controllers;
using AdamFreeman_Ch06_Working_with_VisualStudio.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;

namespace AdamFreeman_Ch07_XUnitTestProject
{
    public class HomeControllerTestsTwo
    {
        [Theory]
        [ClassData(typeof(ProductTestData))]
        public void IndexActionModelIsComplete(Product[] products)
        {
            // Arrange
            var mock = new Mock<IRepository>();
            mock.SetupGet(m => m.Products).Returns(products);
            var controller = new HomeController { Repository = mock.Object };
            // Act
            var model = (controller.Index() as ViewResult)?.ViewData.Model
            as IEnumerable<Product>;
            // Assert
            Assert.Equal(controller.Repository.Products, model,
            Comparer.Get<Product>((p1, p2) => p1.Name == p2.Name
            && p1.Price == p2.Price));
        }
        [Fact]
        public void RepositoryPropertyCalledOnce()
        {
            // Arrange
            var mock = new Mock<IRepository>();
            mock.SetupGet(m => m.Products)
            .Returns(new[] { new Product { Name = "P1", Price = 100 } });
            var controller = new HomeController { Repository = mock.Object };
            // Act
            var result = controller.Index();
            // Assert
            mock.VerifyGet(m => m.Products, Times.Once);
        }
    }
}
