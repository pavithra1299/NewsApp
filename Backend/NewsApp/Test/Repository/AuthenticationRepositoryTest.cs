using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authentications.Models;
using Authentications.Exceptions;
using Authentications.Repository;
using NUnit.Framework;
using Moq;

namespace Test.Repository
{
   public class AuthenticationRepositoryTest
    {
        Login login;

        [SetUp]
        public void setup()
        {
            login = new Login()
            {
                UserId="User1",
                Password="Windows@2023"
            };
        }

        [Fact]
        public void LoginUserShouldReturnTrue()
        {
            Mock<IAuthRepository> mock = new Mock<IAuthRepository>();
            mock.Setup(m => m.LoginUser(login)).Returns(true);
            var result = mock.Object.LoginUser(login);
            Xunit.Assert.True(result);
        }

        [Fact]
        public void LoginUserShouldReturnFalse()
        {
            Mock<IAuthRepository> mock = new Mock<IAuthRepository>();
            mock.Setup(m => m.LoginUser(login)).Returns(false);
            var result = mock.Object.LoginUser(login);
            Xunit.Assert.False(result);
        }

    }
}
