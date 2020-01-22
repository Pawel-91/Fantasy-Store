using FantasyStore.Controllers;
using FantasyStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FantasyStoreTests.ControllerTests
{
    public class AdminControllerTests
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

        [Fact]
        public void Can_Edit_Product()
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
            Product p1 = GetViewModel<Product>(target.Edit(1));
            Product p2 = GetViewModel<Product>(target.Edit(2));
            Product p3 = GetViewModel<Product>(target.Edit(3));

            // Asserts
            Assert.Equal("P1", p1.Name);
            Assert.Equal("P2", p2.Name);
            Assert.Equal("P3", p3.Name);

        }

        [Fact]
        public void Cannot_Edit_Nonexistent_Product()
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
            Product result = GetViewModel<Product>(target.Edit(4));

            // Asserts
            Assert.Null(result);
        }

        [Fact]
        public void Can_Save_Valid_Changes()
        {
            // Arrange - creating repository mock
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            // Arrange - creating mock of TempData
            Mock<ITempDataDictionary> tempData = new Mock<ITempDataDictionary>();

            // Arrange - creating controller
            AdminController target = new AdminController(mock.Object)
            {
                TempData = tempData.Object
            };

            // Arrange - creating product
            Product p = new Product { Name = "Test" };

            // Act - saving product
            IActionResult result = target.Edit(p);

            // Assert - calling repository
            mock.Verify(m => m.SaveProduct(It.IsAny<Product>()), Times.Once);

            // Assert - checking return method type
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", (result as RedirectToActionResult).ActionName);
        }

        [Fact]
        public void Cannot_Save_Invalid_Changes()
        {
            // Arrange - creating repository mock
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            // Arrange - creating controller
            AdminController target = new AdminController(mock.Object);

            // Arrange - creating product
            Product p = new Product { Name = "Test" };

            // Arrange - adding error to model state
            target.ModelState.AddModelError("error", "error");

            // Act - saving product
            IActionResult result = target.Edit(p);

            // Assert - checking if repository was not called
            mock.Verify(m => m.SaveProduct(It.IsAny<Product>()), Times.Never);

            //Assert - cheking return method type
            Assert.IsType<ViewResult>(result);
        }

        private T GetViewModel<T>(IActionResult result) where T : class
         => (result as ViewResult)?.ViewData.Model as T;
    }
}
