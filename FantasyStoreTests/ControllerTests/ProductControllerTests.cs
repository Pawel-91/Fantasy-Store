using FantasyStore.Controllers;
using FantasyStore.Models;
using FantasyStore.Models.ViewModels;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FantasyStoreTests.ControllerTests
{
    public class ProductControllerTests
    {
        [Fact]
        public void Can_Paginate()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
                new Product {ProductID = 4, Name = "P4"},
                new Product {ProductID = 5, Name = "P5"}
                }).AsQueryable());

            ProductController controller = new ProductController(mock.Object)
            {
                PageSize = 3
            };

            // Act
            ProductsListViewModel result =
                controller.List(2).ViewData.Model as ProductsListViewModel;

            // Assert
            Product[] prodArray = result.Products.ToArray();
            Assert.True(prodArray.Length == 2);
            Assert.Equal("P4", prodArray[0].Name);
            Assert.Equal("P5", prodArray[1].Name);
        }

        [Fact]
        public void Can_Send_Pagination_View_Model()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
                new Product {ProductID = 4, Name = "P4"},
                new Product {ProductID = 5, Name = "P5"}
                }).AsQueryable());

            // Arrange

            ProductController controller = new ProductController(mock.Object)
            {
                PageSize = 3
            };

            // Act 
            ProductsListViewModel result =
                controller.List(2).ViewData.Model as ProductsListViewModel;

            // Assert

            PagingInfo pagingInfo = result.PagingInfo;

            Assert.Equal(2, pagingInfo.CurrentPage);
            Assert.Equal(3, pagingInfo.ItemsPerPage);
            Assert.Equal(5, pagingInfo.TotalItems);
            Assert.Equal(2, pagingInfo.TotalPages);
        }
    }
}
