using codestackdemo.Entities;
using codestackdemo.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace codestackdemo.Repositories
{
    public interface IUserRepository
    {
        List<User> GetUsers();
        User GetUser(int id);
        void AddUser(User user);
        User UpdateUser(User user);
        User Login(LoginModel login);
    }
}
