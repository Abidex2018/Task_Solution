using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersManagement.UserModel;
using UsersManagement.UserRepository;

namespace UsersManagement.UserReportRepository
{
    public class UserReportGenerator : IUserReportGenerator
    {
        public string GenerateUserReport(List<User> users)
        {
            if (users.Count == 0)
            {
                return "No users found.";
            }

            var reportBuilder = new StringBuilder("User Report:\n");

            for (int i = 0; i < users.Count; i++)
            {
                reportBuilder.AppendLine($"{i + 1}. {users[i].Name} - {users[i].Email}");
            }

            return reportBuilder.ToString();
        }
    }
}
