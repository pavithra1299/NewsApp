using Authentications.Models;
namespace Authentications.Services
{
    public interface IAuthService
    {
        bool LoginUser(Login user);
       
    }
}

