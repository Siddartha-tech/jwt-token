using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using jtw_token.Entities;
using jtw_token.Helpers;
using jtw_token.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace jtw_token.Services
{
    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<User> _users = new List<User>
        {
            new User { Id = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "test" }
        };
        private readonly IOptions<AppSettings> _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _users.SingleOrDefault(a => a.Username == model.Username && a.Password == model.Password);

            // return null if user not found
            if (user == null)
            {
                return new AuthenticateResponse(new User(), string.Empty);
            }

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);
            return new AuthenticateResponse(user, token);
        }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        public User GetById(int id)
        {
            return _users.FirstOrDefault(x => x.Id == id) ?? new User();
        }

        // helper methods
        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Value.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}