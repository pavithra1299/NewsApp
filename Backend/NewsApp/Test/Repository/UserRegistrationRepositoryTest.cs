using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Xunit;
using UserRegistration.Models;
using UserRegistration.Repository;
using UserRegistration.Exceptions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Test.Repository
{
    public class UserRegistrationRepositoryTest
    {
        User user;

        [SetUp]
        public void Setup()
        {
            user = new User()
            {
                UserId = 3,
                UserName = "Sam",
                FirstName = "Sam",
                LastName = "",
                Email = "Sam@gmail.com",
                Mobile = "2345678901",
                CreatePassword = "Welcome@123",
                ConfirmPassword = "Welcome@123"
            };
        }

        [Fact]
        public void AddUserShouldReturnTrue()
        {

            Mock<IRegisterRepository> mock = new Mock<IRegisterRepository>();
            mock.Setup(m => m.RegisterUser(user)).Returns(true);
            var result = mock.Object.RegisterUser(user);
            Xunit.Assert.True(result);
        }
        [Fact]
        public void AddUserShouldReturnFalse()
        {
            Mock<IRegisterRepository> mock = new Mock<IRegisterRepository>();
            mock.Setup(m => m.RegisterUser(user)).Returns(false);
            var result = mock.Object.RegisterUser(user);
            Xunit.Assert.False(result);
        }
        [Fact]
        public void GetByNameShouldReturnUser()
        {
            var UserName = "Pavithra12";
            Mock<IRegisterRepository> mock = new Mock<IRegisterRepository>();
            mock.Setup(m => m.GetByName(UserName)).Returns(user);
            var result = mock.Object.GetByName(UserName);
            Xunit.Assert.Equal(user, result);
        }

        [Fact]
        public void GetByNameShouldReturnNull()
        {
            var UserName = "ABC";
            Mock<IRegisterRepository> mock = new Mock<IRegisterRepository>();
            mock.Setup(m => m.GetByName(UserName)).Returns((User)null);
            var result = mock.Object.GetByName(UserName);
            Xunit.Assert.Null(result);
        }


    }
}
