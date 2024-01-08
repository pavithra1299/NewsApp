using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authentications.Models;
using Authentications.Services;
using Authentications.Repository;
using Authentications.Exceptions;
using Moq;

namespace Test.Service
{
    public class AuthenticationServiceTest
    {

       
        [Fact]
        public void LoginUser_ValidCredentials_ReturnsTrue()
        {
            // Arrange
            var user = new Login { UserId = "existingUser", Password = "password" };

            var authRepositoryMock = new Mock<IAuthRepository>();
            authRepositoryMock.Setup(repo => repo.LoginUser(user)).Returns(true);

            var authService = new AuthService(authRepositoryMock.Object);

            // Act
            var result = authService.LoginUser(user);

            // Assert
            Assert.True(result);
            authRepositoryMock.Verify(repo => repo.LoginUser(user), Times.Once);
        }

        [Fact]
        public void LoginUser_InvalidCredentials_ReturnsFalse()
        {
            // Arrange
            var user = new Login { UserId = "existingUser", Password = "wrongPassword" };

            var authRepositoryMock = new Mock<IAuthRepository>();
            authRepositoryMock.Setup(repo => repo.LoginUser(user)).Returns(false);

            var authService = new AuthService(authRepositoryMock.Object);

            // Act
            var result = authService.LoginUser(user);

            // Assert
            Assert.False(result);
            authRepositoryMock.Verify(repo => repo.LoginUser(user), Times.Once);
        }
    }
}
