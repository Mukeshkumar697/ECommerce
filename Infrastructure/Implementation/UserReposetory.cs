using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Project.Infrastructure.Service;
using Project.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;

namespace Project.Infrastructure.Implementation
{
    public class UserRepository : IUser
    {
        private readonly ProjDbContext context;
        private readonly IConfiguration configuration;

        public UserRepository(ProjDbContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public List<User> GetAll()
        {
            return context.Users.ToList();
        }

        public User AddUser(User user)
        {
            if (UserExist(user))
            {
                return null;
            }
            else
            {
                var result = context.Users.Add(user);
                context.SaveChangesAsync();
                return result.Entity;
            }
        }

 
        public string Login(string email, string password)
        {
            var userExist = context.Users.FirstOrDefault(t => t.Email == email && EF.Functions.Collate(t.Password, "SQL_Latin1_General_CP1_CS_AS") == password);
            if (userExist != null)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var claims = new[]
                {
            new Claim(ClaimTypes.Email,userExist.Email),
            new Claim("UserId",userExist.UserId.ToString()),
            new Claim(ClaimTypes.Role,userExist.Role)
        };
                var token = new JwtSecurityToken(configuration["Jwt:Issuer"], configuration["Jwt:Audience"], claims, expires: DateTime.Now.AddMinutes(30), signingCredentials: credentials);
                return new JwtSecurityTokenHandler().WriteToken(token);

            }
            return null;
        }

        public User UpdateUserDetail(int id, User user)
        {
            var result = context.Users.FirstOrDefault(t => t.UserId == id);
            if (result != null)
            {
                result.Name = user.Name;
                result.Email = user.Email;
                result.Password = user.Password;
                result.PhoneNumber = user.PhoneNumber;
                result.Address = user.Address;

                context.SaveChanges();
                return result;
            }
            return null;
        }


        

        private bool UserExist(User user)
        {
            return context.Users.Any(t => t.Email == user.Email);
        }
    }

   
}
