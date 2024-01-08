using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationService.Models;

namespace AuthenticationService.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AuthDbContext context;

        public AuthRepository(AuthDbContext dbContext)
        {
            context = dbContext;
        }

        public bool CreateUser(User user)
        {
            context.Users.Add(user);
            return context.SaveChanges()>0;
        }

        public bool IsUserExists(string userId)
        {
            return context.Users.Find(userId)!=null;
        }

        public bool LoginUser(User user)
        {
            var _user= context.Users.FirstOrDefault(u => u.UserId == user.UserId && u.Password == user.Password);
            if (_user != null)
                return true;
            else
                return false;
        }
    }
}
