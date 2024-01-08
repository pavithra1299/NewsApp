using AuthenticationService.Exceptions;
using AuthenticationService.Models;
using AuthenticationService.Repository;

namespace AuthenticationService.Service
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository repository;

        public AuthService(IAuthRepository authRepository)
        {
            repository = authRepository;
        }

        public bool RegisterUser(User user)
        {
            if (repository.IsUserExists(user.UserId))
            {
                throw new UserAlreadyExistsException($"This userId {user.UserId} already in use");
            }
            else
            {
                return repository.CreateUser(user);
            }
        }

        public bool LoginUser(User user)
        {
            return repository.LoginUser(user);
        }
    }
}
