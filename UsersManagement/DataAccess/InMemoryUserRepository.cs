using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersManagement.UserModel;
using UsersManagement.UserRepository;

namespace UsersManagement.DataAccess
{
    public class InMemoryUserRepository : IUserRepository
    {
        private readonly List<User> _users = new();

        public void AddUser(User user)
        {
            if (UserExists(user.Email))
                throw new InvalidOperationException($"User with email {user.Email} already exists.");

            _users.Add(user);
            Console.WriteLine($"User {user.Name} with email {user.Email} added.");
        }

        public void RemoveUser(string email)
        {
            var user = _users.FirstOrDefault(u => u.Email == email);

            if (user == null)
                throw new KeyNotFoundException($"No user found with email {email}.");

            _users.Remove(user);
        }

        //public void UpdateUser(string currentEmail, string newName, string newEmail)
        //{
        //    var user = _users.FirstOrDefault(u => u.Email == currentEmail);
        //    if (user == null)
        //        throw new KeyNotFoundException($"No user found with email {currentEmail}.");

        //    if (!string.IsNullOrWhiteSpace(newEmail) && newEmail != currentEmail && UserExists(newEmail))
        //        throw new InvalidOperationException($"A user with the email {newEmail} already exists.");

        //    user.Name = !string.IsNullOrWhiteSpace(newName) ? newName : user.Name;
        //    user.Email = !string.IsNullOrWhiteSpace(newEmail) ? newEmail : user.Email;
        //}

        public bool UserExists(string email) => _users.Any(u => u.Email == email);
        public List<User> GetAllUsers() => new List<User>(_users);
    }
}
