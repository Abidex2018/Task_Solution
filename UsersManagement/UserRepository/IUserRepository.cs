using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersManagement.UserModel;

namespace UsersManagement.UserRepository
{
    public interface IUserRepository
    {
        void AddUser(User user);
        void RemoveUser(string email);
        //void UpdateUser(string currentEmail, string newName, string newEmail);
        bool UserExists(string email);
        List<User> GetAllUsers();
    }
}
