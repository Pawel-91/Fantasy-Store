using FantasyStore.Controllers;
using FantasyStore.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FantasyStoreTests.ControllerTests
{
    public class AdminsControllerTests
    {
        [Fact]
        public void Index_Contains_All_Products()
        {
            // Arrange - creating repository mock
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product { ProductID = 1, Name = "P1" },
                new Product { ProductID = 2, Name = "P2" },
                new Product { ProductID = 3, Name = "P3"}
            }.AsQueryable());

            // Arrange - creating controller
            AdminController target = new AdminController(mock.Object);

            // Act
            Product[] result = GetViewModel<IEnumerable<Product>>(target.Index())?.ToArray();

            // Assert
            Assert.Equal(3, result.Length);
            Assert.Equal("P1", result[0]?.Name);
            Assert.Equal("P2", result[1]?.Name);
            Assert.Equal("P3", result[2]?.Name);
        }

        private T GetViewModel<T>(IActionResult result) where T : class
         => (result as ViewResult)?.ViewData.Model as T;
    }
}
