using System;
using System.Collections.Generic;
using UserRegistration.Models;
namespace UserRegistration.Repository
{
    public interface IRegisterRepository
    {
        public bool RegisterUser(User user);
        public User GetByName(string name);
      
    }
}
