using jtw_token.Entities;
using jtw_token.Models;

namespace jtw_token.Services
{
    public interface IUserService
    {
         AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }
}