using UserRegistration.Models;

namespace UserRegistration.Services
{
    public interface IRegisterService
    {
        public bool RegisterUser(User user);
        public User GetByName(string name);
       
    }
}
