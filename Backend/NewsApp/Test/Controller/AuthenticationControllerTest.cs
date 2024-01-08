using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authentications.Models;
using Authentications.Controllers;
using Authentications.Exceptions;
using Authentications.Services;
using Moq;
using Xunit;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace Test.Controller
{
  public class AuthenticationControllerTest
    {
       
       

        [Fact]
        public void Login_ValidUser_ReturnsOkWithToken()
        {
            // Arrange
            var authServiceMock = new Mock<IAuthService>();
            authServiceMock.Setup(service => service.LoginUser(It.IsAny<Login>())).Returns(true);

            var loggerMock = new Mock<ILogger<AuthController>>();
            var authController = new AuthController(authServiceMock.Object, loggerMock.Object);

            // Act
            var user = new Login { UserId = "TestUser", Password = "TestPassword" };
            var result = authController.Login(user) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Value);
            authServiceMock.Verify(service => service.LoginUser(user), Times.Once);
        }

        [Fact]
        public void Login_InvalidUser_ReturnsUnauthorized()
        {
            // Arrange
            var authServiceMock = new Mock<IAuthService>();
            authServiceMock.Setup(service => service.LoginUser(It.IsAny<Login>())).Returns(false);

            var loggerMock = new Mock<ILogger<AuthController>>();
            var authController = new AuthController(authServiceMock.Object, loggerMock.Object);

            // Act
            var user = new Login { UserId = "InvalidUser", Password = "TestPassword" };
            var result = authController.Login(user) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(401, result.StatusCode);
            Assert.Equal("Invalid user id or password", result.Value);
            authServiceMock.Verify(service => service.LoginUser(user), Times.Once);
        }

        [Fact]
        public void Login_InternalServerError_ReturnsInternalServerError()
        {
            // Arrange
            var authServiceMock = new Mock<IAuthService>();
            authServiceMock.Setup(service => service.LoginUser(It.IsAny<Login>())).Throws(new Exception("Simulated error"));

            var loggerMock = new Mock<ILogger<AuthController>>();
            var authController = new AuthController(authServiceMock.Object, loggerMock.Object);

            // Act
            var user = new Login { UserId = "ErrorUser", Password = "TestPassword" };
            var result = authController.Login(user) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
            Assert.Equal("Something went wrong, please try later Simulated error", result.Value);
            authServiceMock.Verify(service => service.LoginUser(user), Times.Once);
        }
    }
}
