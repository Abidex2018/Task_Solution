using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersManagement.UserModel;

namespace UsersManagement.UserRepository
{
    public interface IUserReportGenerator
    {
        string GenerateUserReport(List<User> users);
    }
}
