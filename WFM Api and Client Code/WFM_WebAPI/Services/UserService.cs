using Microsoft.Extensions.Options;

using System.Collections.Generic;

using System.Security.Claims;
using System.Text;
using System;
using WFM_WebAPI.Models;
using System.Linq;
using WFM_WebAPI.Helpers;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace WFM_WebAPI.Services
{
   
        public interface IUserService
        {
            AuthenticateResponse Authenticate(AuthenticateRequest model);
            IEnumerable<User> GetAllUsers();
            User GetById(int id);
        }

        public class UserService : IUserService
        {
            private readonly SQL_DBContext _context;

            private readonly AppSettings _appSettings;

            public UserService(IOptions<AppSettings> appSettings, SQL_DBContext sQL_DBContext)
            {
                _appSettings = appSettings.Value;
                _context = sQL_DBContext;
            }

            public IEnumerable<User> GetAllUsers()
            {
                var result = _context.Users.ToList();
                return result;
           
            }
            
    
    
            public AuthenticateResponse Authenticate(AuthenticateRequest model)
             {
                var user = GetAllUsers().SingleOrDefault(x => x.username == model.Username && x.password == model.Password);

                // return null if user not found
                if (user == null) return null;

                // authentication successful so generate jwt token
                var token = generateJwtToken(user);

                return new AuthenticateResponse(user, token);
            }



            public User GetById(int id)
            {
                return GetAllUsers().FirstOrDefault(x => x.Id == id);
            }

            // helper methods

            private string generateJwtToken(User user)
            {
                // generate token that is valid for 7 days
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("This is my first Security key and hope this is enough to create jwt token");
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

