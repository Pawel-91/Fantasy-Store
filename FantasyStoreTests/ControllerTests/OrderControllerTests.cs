using FantasyStore.Controllers;
using FantasyStore.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace FantasyStoreTests.ControllerTests
{
    public class OrderControllerTests
    {
        [Fact]
        public void Cannot_Checkout_Empty_Cart()
        {
            // Arrange - creating repo moq
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            // Arrange - creating empty cart
            Cart cart = new Cart();
            // Arrange - creating order
            Order order = new Order();
            //Arrange - creating controller object
            OrderController target = new OrderController(mock.Object, cart);

            // Act
            ViewResult result = target.Checkout(order) as ViewResult;

            // Assert - check, if order is placed in the repository
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never());
            // Assert - check, if method returns default view
            Assert.True(string.IsNullOrEmpty(result.ViewName));
            // Assert - check, if we give valid model to view
            Assert.False(result.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void Cannot_Checkout_Invalid_ShippingDetails()
        {
            // Arrange - creating repo moq
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            // Arrange - creating cart with product
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);
            
            //Arrange - creating controller object
            OrderController target = new OrderController(mock.Object, cart);
            // Arrange - adding error to model
            target.ModelState.AddModelError("error", "error");

            // Act - attempt to finish the order
            ViewResult result = target.Checkout(new Order()) as ViewResult;

            // Assert - check, if order was not passed to the repository
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never());
            // Assert - check, if method returns default view
            Assert.True(string.IsNullOrEmpty(result.ViewName));
            // Assert - check if invalid model is passed to view model
            Assert.False(result.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void Can_Checkout_And_Submit_Order()
        {
            // Arrange - creating repo moq
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            // Arrange - creating cart with product
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);

            //Arrange - creating controller object
            OrderController target = new OrderController(mock.Object, cart);

            // Act - attempt to finish the order
            RedirectToActionResult result = target.Checkout(new Order()) as RedirectToActionResult;

            // Assert - check, if order was passed to the repository
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Once());
            // Assert - check, if method redirects to action Completed
            Assert.Equal("Completed", result.ActionName);
        }
    }
}
