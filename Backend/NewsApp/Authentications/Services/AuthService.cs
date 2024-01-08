using Authentications.Repository;
using Authentications.Models;
using Authentications.Exceptions;
namespace Authentications.Services
{
        public class AuthService : IAuthService
        {
            private readonly IAuthRepository repository;

            public AuthService(IAuthRepository authRepository)
            {
                repository = authRepository;
            }

            

            public bool LoginUser(Login user)
            {
                return repository.LoginUser(user);
            }

        }
}
