using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authentications.Models;
using UserRegistration.Repository;

namespace Authentications.Repository
{
    public class AuthRepository:IAuthRepository
    {

        private readonly UserDbContext context;

        public AuthRepository(UserDbContext dbContext)
        {
            context = dbContext;
        }

       

        public bool LoginUser(Login user)
        {
            var _user = context.Users.FirstOrDefault(u => u.UserName == user.UserId && u.CreatePassword == user.Password);
            if (_user != null)
                return true;
            else
                return false;
        }

    }
}
