using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using UserRegistration.Models;

namespace UserRegistration.Repository
{
    public class RegisterRepository : IRegisterRepository
    {
        private readonly UserDbContext _dbContext;

        public RegisterRepository(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        public User GetByName(string name)
        {
           
            return _dbContext.Users.FirstOrDefault(x => x.UserName == name);
        }

        
       

        public bool RegisterUser(User user)
        {
           
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return true;
        }
    }
}
