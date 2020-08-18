using codestackdemo.Entities;
using codestackdemo.Entities.Models;
using codestackdemo.Extensions;
using codestackdemo.Extensions.Exceptions;
using codestackdemo.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace codestackdemo.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IConfiguration _config;

        public UserService(IUserRepository repository, IConfiguration config)
        {
            _repository = repository;
            _config = config;
        }

        public User Login(LoginModel login)
        {
            var user = _repository.Login(login);
            if (!user.IsNull() && Hash.VerifyPassword(user.Password, login.Password))
            {
                if (login.Admin && user.RoleId != 1)
                    throw new ExceptionModel(403, "You don't have enough permissions");

                user.Token = WriteToken(user.UserId, user.RoleId);
                user.Password = null;
                return user;
            }
            throw new ExceptionModel(401, "You password or email is incorrect");
        }

        private string WriteToken(int userId, int userRoleId)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["TokenSigning"]));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                issuer: _config["ClientUrl"],
                audience: _config["ClientUrl"],
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials,
                claims: new List<Claim>
                {
                    new Claim("UserId", userId.ToString()),
                    new Claim(ClaimTypes.Role, userRoleId.ToString())
                });
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        public List<User> GetUsers()
        {
            return _repository.GetUsers();
        }

        public void AddUser(User user)
        {
            user.Password = Hash.HashPassword(user.Password);
            _repository.AddUser(user);
        }

        public void DeleteUser(int id)
        {
            var user = _repository.GetUser(id);
            if (user.IsNull())
                throw new ExceptionModel(400, "User not found");
            user.IsDeleted = true;
            _repository.UpdateUser(user);
        }
    }
}
