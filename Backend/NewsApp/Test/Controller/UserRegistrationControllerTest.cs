using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using UserRegistration.Models;
using UserRegistration.Services;
using UserRegistration.Controllers;
using UserRegistration.Exceptions;
using Microsoft.Extensions.Logging;

namespace Test.Controller
{
    public class UserRegistrationControllerTest
    {
        [Fact]
        public void CreateUser_ValidUser_ReturnsCreated()
        {
            // Arrange
            var registerServiceMock = new Mock<IRegisterService>();
            registerServiceMock.Setup(service => service.RegisterUser(It.IsAny<User>())).Returns(true);

            var loggerMock = new Mock<ILogger<RegisterController>>();
            var registerController = new RegisterController(registerServiceMock.Object, loggerMock.Object);

            // Act
            var user = new User { UserName = "TestUser" };
            var result = registerController.CreateUser(user) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(201, result.StatusCode);
            Assert.True((bool)result.Value);
            registerServiceMock.Verify(service => service.RegisterUser(user), Times.Once);
        }

        [Fact]
        public void CreateUser_DuplicateUser_ReturnsConflict()
        {
            // Arrange
            var registerServiceMock = new Mock<IRegisterService>();
            registerServiceMock.Setup(service => service.RegisterUser(It.IsAny<User>())).Throws(new UserNotCreatedException("User already exists"));

            var loggerMock = new Mock<ILogger<RegisterController>>();
            var registerController = new RegisterController(registerServiceMock.Object, loggerMock.Object);

            // Act
            var user = new User { UserName = "DuplicateUser" };
            var result = registerController.CreateUser(user) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(409, result.StatusCode);
            Assert.Equal("User already exists", result.Value);
            registerServiceMock.Verify(service => service.RegisterUser(user), Times.Once);
        }

        [Fact]
        public void CreateUser_InternalServerError_ReturnsInternalServerError()
        {
            // Arrange
            var registerServiceMock = new Mock<IRegisterService>();
            registerServiceMock.Setup(service => service.RegisterUser(It.IsAny<User>())).Throws(new Exception("Simulated error"));

            var loggerMock = new Mock<ILogger<RegisterController>>();
            var registerController = new RegisterController(registerServiceMock.Object, loggerMock.Object);

            // Act
            var user = new User { UserName = "ErrorUser" };
            var result = registerController.CreateUser(user) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
            Assert.Equal("Internal Server Error: Simulated error", result.Value);
            registerServiceMock.Verify(service => service.RegisterUser(user), Times.Once);
        }

        [Fact]
        public void GetUserByName_ExistingUser_ReturnsOk()
        {
            // Arrange
            var registerServiceMock = new Mock<IRegisterService>();
            registerServiceMock.Setup(service => service.GetByName(It.IsAny<string>())).Returns(new User { UserName = "ExistingUser" });

            var loggerMock = new Mock<ILogger<RegisterController>>();
            var registerController = new RegisterController(registerServiceMock.Object, loggerMock.Object);

            // Act
            var result = registerController.GetUserByName("ExistingUser") as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.IsType<User>(result.Value);
            registerServiceMock.Verify(service => service.GetByName("ExistingUser"), Times.Once);
        }

        [Fact]
        public void GetUserByName_UserNotFound_ReturnsNotFound()
        {
            // Arrange
            var registerServiceMock = new Mock<IRegisterService>();
            registerServiceMock.Setup(service => service.GetByName(It.IsAny<string>())).Throws(new UserNotFoundException("User not found"));

            var loggerMock = new Mock<ILogger<RegisterController>>();
            var registerController = new RegisterController(registerServiceMock.Object, loggerMock.Object);

            // Act
            var result = registerController.GetUserByName("NonExistingUser") as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
            Assert.Equal("User not found", result.Value);
            registerServiceMock.Verify(service => service.GetByName("NonExistingUser"), Times.Once);
        }

        [Fact]
        public void GetUserByName_InternalServerError_ReturnsInternalServerError()
        {
            // Arrange
            var registerServiceMock = new Mock<IRegisterService>();
            registerServiceMock.Setup(service => service.GetByName(It.IsAny<string>())).Throws(new Exception("Simulated error"));

            var loggerMock = new Mock<ILogger<RegisterController>>();
            var registerController = new RegisterController(registerServiceMock.Object, loggerMock.Object);

            // Act
            var result = registerController.GetUserByName("ErrorUser") as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
            Assert.Equal("Internal Server Error: Simulated error", result.Value);
            registerServiceMock.Verify(service => service.GetByName("ErrorUser"), Times.Once);
        }

    }
}
