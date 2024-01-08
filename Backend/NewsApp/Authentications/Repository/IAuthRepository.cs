using Authentications.Models;
namespace Authentications.Repository
{
    public interface IAuthRepository
    {
    
        bool LoginUser(Login user);
    }
}
