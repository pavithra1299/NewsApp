using System;
using System.Collections.Generic;
using UserRegistration.Models;
using UserRegistration.Exceptions;
using UserRegistration.Repository;

namespace UserRegistration.Services
{
   
    public class RegisterService:IRegisterService
    {
        private readonly IRegisterRepository _registerRepository;

        public RegisterService(IRegisterRepository registerRepository)
        {
            _registerRepository = registerRepository;
        }
         

        public bool RegisterUser(User user)
        {
            var ExistingUser = _registerRepository.GetByName(user.UserName);
            if(ExistingUser == null)
            {
                return _registerRepository.RegisterUser(user);
            }
            else
            {
                throw new UserNotCreatedException($"User with username {user.UserName} already exists");
            }
        }
        public User GetByName(string userName)
        {
            var user = _registerRepository.GetByName(userName);
            if(user != null)
            {
                return user;
            }
            else
            {
                throw new UserNotFoundException($"User with username {userName} not found");
            }


        }
       
    }
}
