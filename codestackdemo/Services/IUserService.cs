using codestackdemo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace codestackdemo.Services
{
    public interface IUserService
    {
        List<User> GetUsers();
        void AddUser(User user);
        void DeleteUser(int id);
    }
}
