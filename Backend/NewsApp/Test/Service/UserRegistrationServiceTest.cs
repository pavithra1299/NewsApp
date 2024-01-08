using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRegistration.Exceptions;
using UserRegistration.Models;
using UserRegistration.Repository;
using UserRegistration.Services;

namespace Test
{
    public class UserRegistrationServiceTest
    {
        [Fact]
        public void RegisterUser_Success()
        {
            // Arrange
            var user = new User { 
                UserName = "Pavithra12" ,
                FirstName = "Pavithra",
                LastName = "V",
                Email = "abc@gmail.com",
                Mobile ="1234567890",
                CreatePassword="Welcome@2023",
                ConfirmPassword="Welcome@2023"
            };
            var mockRepository = new Mock<IRegisterRepository>();
            mockRepository.Setup(repo => repo.GetByName(user.UserName)).Returns((User)null);
            mockRepository.Setup(repo => repo.RegisterUser(user)).Returns(true);

            var userService = new RegisterService(mockRepository.Object);

            // Act
            var result = userService.RegisterUser(user);

            // Assert
            Assert.NotNull(result);
            Assert.True(result);
        }
        [Fact]
        public void RegisterUser_UserAlreadyExists()
        {
            // Arrange
            var user = new User {
                UserId = 2,
                UserName = "Pavithra12",
                FirstName = "Pavithra",
                LastName = "V",
                Email = "abc@gmail.com",
                Mobile = "1234567890",
                CreatePassword = "Welcome@2023",
                ConfirmPassword = "Welcome@2023"
            };
            var existingUser = new User {
                UserId=1,
                UserName = "Pavithra12",
                FirstName = "Pavithra",
                LastName = "V",
                Email = "abc@gmail.com",
                Mobile = "1234567890",
                CreatePassword = "Welcome@2023",
                ConfirmPassword = "Welcome@2023"
            
            };
            var mockRepository = new Mock<IRegisterRepository>();
            mockRepository.Setup(repo => repo.GetByName(user.UserName)).Returns(existingUser);

            var userService = new RegisterService(mockRepository.Object);

            // Act & Assert
            var exception = Assert.Throws<UserNotCreatedException>(() => userService.RegisterUser(user));
            Assert.Equal($"User with username {user.UserName} already exists", exception.Message);
        }
        [Fact]
        public void GetByName_UserFound()
        {
            // Arrange
            var userName = "Pavithra12";
            var user = new User {
                UserName = "Pavithra12",
                FirstName = "Pavithra",
                LastName = "V",
                Email = "abc@gmail.com",
                Mobile = "1234567890",
                CreatePassword = "Welcome@2023",
                ConfirmPassword = "Welcome@2023"
            };
            var mockRepository = new Mock<IRegisterRepository>();
            mockRepository.Setup(repo => repo.GetByName(userName)).Returns(user);

            var userService = new RegisterService(mockRepository.Object);

            // Act
            var result = userService.GetByName(userName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user, result);
        }

        [Fact]
        public void GetByName_UserNotFound()
        {
            // Arrange
            var userName = "User1";
            var mockRepository = new Mock<IRegisterRepository>();
            mockRepository.Setup(repo => repo.GetByName(userName)).Returns((User)null);

            var userService = new RegisterService(mockRepository.Object);

            // Act & Assert
            var exception = Assert.Throws<UserNotFoundException>(() => userService.GetByName(userName));
            Assert.Equal($"User with username {userName} not found", exception.Message);
        }


    }
}