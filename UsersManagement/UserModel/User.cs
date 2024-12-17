using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersManagement.UserModel
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public User(string name, string email)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name), "Name cannot be null.");
            Email = email ?? throw new ArgumentNullException(nameof(email), "Email cannot be null.");
        }
    }
}
