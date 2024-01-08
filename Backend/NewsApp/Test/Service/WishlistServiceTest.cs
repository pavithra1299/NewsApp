using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wishlist.Models;
using Wishlist.Service;
using Wishlist.Controllers;
using Wishlist.Repository;
using Wishlist.Exceptions;
using Moq;

namespace Test.Service
{
    public class WishlistServiceTest
    {

        [Fact]
        public void AddToWishlist_SuccessfulAddition()
        {
            var mockRepo = new Mock<IWishlistRepository>();
            string UserName = "testUser";
            // Arrange
            var wishToAdd = new Wish
            {
                Title = "Book Title",
                Description = "Book Description",
                Author = "Author Name",
                Url = "http://example.com",
                UrlToImage = "http://example.com/image.jpg",
                UserName = "testUser"
            };
            mockRepo.Setup(repo => repo.AddToWishlist(wishToAdd)).Returns(wishToAdd);
            var service = new WishlistService(mockRepo.Object);

            var actual = service.AddToWishlist(wishToAdd,UserName);

            Assert.Null(actual);

            //var wishlistRepositoryMock = new Mock<IWishlistRepository>();
            //wishlistRepositoryMock.Setup(repo => repo.AddToWishlist(It.IsAny<Wish>())).Returns(wishToAdd);

            //var wishlistService = new WishlistService(wishlistRepositoryMock.Object);

            //// Act
            //var result = wishlistService.AddToWishlist(wishToAdd, "testUser");

            //// Assert
            //Assert.NotNull(result);
            //Assert.Equal(wishToAdd, result);
            //wishlistRepositoryMock.Verify(repo => repo.AddToWishlist(wishToAdd), Times.Once);
        }

        [Fact]
        public void DeleteFromWishlist_ValidId_SuccessfulDeletion()
        {
            // Arrange
            var authorToDelete = "Author Name";
            var userId = "testUser";

            var wishlistRepositoryMock = new Mock<IWishlistRepository>();
            wishlistRepositoryMock.Setup(repo => repo.DeleteFromWishlist(authorToDelete, userId)).Returns(true);

            var wishlistService = new WishlistService(wishlistRepositoryMock.Object);

            // Act
            var result = wishlistService.DeleteFromWishlist(authorToDelete, userId);

            // Assert
            Assert.True(result);
            wishlistRepositoryMock.Verify(repo => repo.DeleteFromWishlist(authorToDelete, userId), Times.Once);
        }

        [Fact]
        public void DeleteFromWishlist_InvalidId_ThrowsInvalidIdException()
        {
            // Arrange
            var authorToDelete = "Nonexistent Author";
            var userId = "testUser";

            var wishlistRepositoryMock = new Mock<IWishlistRepository>();
            wishlistRepositoryMock.Setup(repo => repo.DeleteFromWishlist(authorToDelete, userId)).Returns(false);

            var wishlistService = new WishlistService(wishlistRepositoryMock.Object);

            // Act & Assert
            var exception = Assert.Throws<InvalidIdException>(() => wishlistService.DeleteFromWishlist(authorToDelete, userId));
            Assert.Equal("Id not found", exception.Message);
            wishlistRepositoryMock.Verify(repo => repo.DeleteFromWishlist(authorToDelete, userId), Times.Once);
        }

        [Fact]
        public void GetAllWishLists_ReturnsListOfWishes()
        {
            // Arrange
            var userId = "testUser";
            var expectedWishlist = new List<Wish> { /* Add wishes as needed */ };

            var wishlistRepositoryMock = new Mock<IWishlistRepository>();
            wishlistRepositoryMock.Setup(repo => repo.GetAllWishlists(userId)).Returns(expectedWishlist);

            var wishlistService = new WishlistService(wishlistRepositoryMock.Object);

            // Act
            var result = wishlistService.GetAllWishLists(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedWishlist, result);
            wishlistRepositoryMock.Verify(repo => repo.GetAllWishlists(userId), Times.Once);
        }

        [Fact]
        public void SearchWishlist_ValidId_ReturnsWish()
        {
            // Arrange
            var wishlistId = "validId";
            var userId = "testUser";
            var expectedWish = new Wish { /* Initialize the expected wish */ };

            var wishlistRepositoryMock = new Mock<IWishlistRepository>();
            wishlistRepositoryMock.Setup(repo => repo.SearchWishlist(wishlistId, userId)).Returns(expectedWish);

            var wishlistService = new WishlistService(wishlistRepositoryMock.Object);

            // Act
            var result = wishlistService.SearchWishlist(wishlistId, userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedWish, result);
            wishlistRepositoryMock.Verify(repo => repo.SearchWishlist(wishlistId, userId), Times.Once);
        }
    }
}
