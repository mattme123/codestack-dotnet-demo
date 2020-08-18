using codestackdemo.DataAccess;
using codestackdemo.Entities;
using codestackdemo.Entities.Models;
using codestackdemo.Extensions.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace codestackdemo.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }

        public User Login(LoginModel login)
        {
            return _context.Users
                .Select(x => new User
                {
                    UserId = x.UserId,
                    Password = x.Password,
                    Email = x.Email,
                    RoleId = x.RoleId
                })
                .FirstOrDefault(x => x.Email == login.Email);
        }

        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUser(int id)
        {
            return _context.Users.Include(x => x.Role).FirstOrDefault(x => x.UserId == id);
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
            return user;
        }
    }
}
