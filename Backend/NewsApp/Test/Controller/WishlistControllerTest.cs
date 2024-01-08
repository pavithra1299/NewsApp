using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wishlist.Controllers;
using Wishlist.Exceptions;
using Wishlist.Models;
using Wishlist.Service;
using Wishlist.Repository;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace Test.Controller
{
    public class WishlistControllerTest
    {

        [Fact]
        public void AddToWishList_ValidInput_ReturnsCreatedAtAction()
        {
            // Arrange
            var wishlistServiceMock = new Mock<IWishlistService>();
            var loggerMock = new Mock<ILogger<WishlistController>>();
            var controller = new WishlistController(wishlistServiceMock.Object, loggerMock.Object);

            // Act
            var result = controller.AddToWishList(new Wish(), "testUser");

            // Assert
            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public void DeleteFromWishlist_ValidInput_ReturnsOk()
        {
            // Arrange
            var wishlistServiceMock = new Mock<IWishlistService>();
            wishlistServiceMock.Setup(service => service.DeleteFromWishlist(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            var loggerMock = new Mock<ILogger<WishlistController>>();
            var controller = new WishlistController(wishlistServiceMock.Object, loggerMock.Object);

            // Act
            var result = controller.DeleteFromWishlist("author", "testUser");

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetWishList_ValidInput_ReturnsOk()
        {
            // Arrange
            var wishlistServiceMock = new Mock<IWishlistService>();
            wishlistServiceMock.Setup(service => service.SearchWishlist(It.IsAny<string>(), It.IsAny<string>())).Returns(new Wish());
            var loggerMock = new Mock<ILogger<WishlistController>>();
            var controller = new WishlistController(wishlistServiceMock.Object, loggerMock.Object);

            // Act
            var result = controller.GetWishList("id", "testUser");

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetAllWishLists_ValidInput_ReturnsOk()
        {
            // Arrange
            var wishlistServiceMock = new Mock<IWishlistService>();
            wishlistServiceMock.Setup(service => service.GetAllWishLists(It.IsAny<string>())).Returns(new System.Collections.Generic.List<Wish>());
            var loggerMock = new Mock<ILogger<WishlistController>>();
            var controller = new WishlistController(wishlistServiceMock.Object, loggerMock.Object);

            // Act
            var result = controller.GetAllWishLists("testUser");

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        // Negative test cases

        [Fact]
        public void AddToWishList_InvalidInput_ReturnsBadRequest()
        {
            // Arrange
            var wishlistServiceMock = new Mock<IWishlistService>();
            var loggerMock = new Mock<ILogger<WishlistController>>();
            var controller = new WishlistController(wishlistServiceMock.Object, loggerMock.Object);

            // Act
            var result = controller.AddToWishList(null, "testUser");

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void DeleteFromWishlist_InvalidInput_ReturnsNotFound()
        {
            // Arrange
            var wishlistServiceMock = new Mock<IWishlistService>();
            wishlistServiceMock.Setup(service => service.DeleteFromWishlist(It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            var loggerMock = new Mock<ILogger<WishlistController>>();
            var controller = new WishlistController(wishlistServiceMock.Object, loggerMock.Object);

            // Act
            var result = controller.DeleteFromWishlist("author", "testUser");

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void GetWishList_InvalidInput_ReturnsNotFound()
        {
            // Arrange
            var wishlistServiceMock = new Mock<IWishlistService>();
            wishlistServiceMock.Setup(service => service.SearchWishlist(It.IsAny<string>(), It.IsAny<string>())).Returns((Wish)null);
            var loggerMock = new Mock<ILogger<WishlistController>>();
            var controller = new WishlistController(wishlistServiceMock.Object, loggerMock.Object);

            // Act
            var result = controller.GetWishList("id", "testUser");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetAllWishLists_InvalidInput_ReturnsNotFound()
        {
            // Arrange
            var wishlistServiceMock = new Mock<IWishlistService>();
            wishlistServiceMock.Setup(service => service.GetAllWishLists(It.IsAny<string>())).Returns((System.Collections.Generic.List<Wish>)null);
            var loggerMock = new Mock<ILogger<WishlistController>>();
            var controller = new WishlistController(wishlistServiceMock.Object, loggerMock.Object);

            // Act
            var result = controller.GetAllWishLists("testUser");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
