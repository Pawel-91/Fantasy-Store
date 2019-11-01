using FantasyStore.Components;
using FantasyStore.Models;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FantasyStoreTests.ComponentTests
{
    public class NavigationMenuTests
    {
        [Fact]
        public void Can_Select_Categories()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product {ProductID = 1, Name = "P1", Category = "Apples"},
                new Product {ProductID = 2, Name = "P2", Category = "Apples"},
                new Product {ProductID = 3, Name = "P3", Category = "Plums"},
                new Product {ProductID = 4, Name = "P4", Category = "Oranges"},
                }).AsQueryable<Product>());

            NavigationMenuViewComponent target = 
                new NavigationMenuViewComponent(mock.Object);

            // Act
            string[] results = ((IEnumerable<string>)(target.Invoke() as ViewViewComponentResult)
                .ViewData.Model).ToArray();

            // Assert
            Assert.True(Enumerable.SequenceEqual(results,
                new string[] { "Apples", "Oranges", "Plums" }));
        }

        [Fact]
        public void Indicates_Selected_Category()
        {
            // Assert
            string selectedCategory = "Apples";
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product {ProductID = 1, Name = "P1", Category = "Apples"},
                new Product {ProductID = 2, Name = "P2", Category = "Apples"},
                new Product {ProductID = 3, Name = "P3", Category = "Plums"},
                new Product {ProductID = 4, Name = "P4", Category = "Oranges"},
                }).AsQueryable<Product>());

            NavigationMenuViewComponent target =
                new NavigationMenuViewComponent(mock.Object)
                {
                    ViewComponentContext = new ViewComponentContext
                    {
                        ViewContext = new Microsoft.AspNetCore.Mvc.Rendering.ViewContext
                        {
                            RouteData = new Microsoft.AspNetCore.Routing.RouteData()
                        }
                    }
                };

            target.RouteData.Values["category"] = selectedCategory;

            // Act
            string result = (string)(target.Invoke() as ViewViewComponentResult)
                .ViewData["CurrentCategory"];

            // Assert
            Assert.Equal(selectedCategory, result);

        }
    }
}
