using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersManagement.UserModel;
using UsersManagement.UserReportRepository;
using UsersManagement.UserRepository;

namespace UsersManagement.UserManager
{
    public class UserManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserReportGenerator _userReportGenerator;

        public UserManager(IUserRepository userRepository, IUserReportGenerator userReportGenerator)
        {
            _userRepository = userRepository;
            _userReportGenerator = userReportGenerator;
        }

        public void AddUser(User user)
        {
            try
            {
                var userInfo = new User(user.Name, user.Email);
                _userRepository.AddUser(user);
                Console.WriteLine($"User {user.Name} with email {user.Email} added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding user: {ex.Message}");
                
            }
        }

        public void RemoveUser(string email)
        {
            try
            {
                _userRepository.RemoveUser(email);
                Console.WriteLine($"User with email {email} removed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing user: {ex.Message}");
            }
        }

        //public void UpdateUser(string currentEmail, string newName, string newEmail)
        //{
        //    try
        //    {
        //        _userRepository.UpdateUser(currentEmail, newName, newEmail);
        //        Console.WriteLine($"User with email {currentEmail} updated successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error updating user: {ex.Message}");
        //       
              
        //    }
        //}

        public void GenerateAndPrintReport()
        {
            try
            {
                var users = _userRepository.GetAllUsers();
                var report = _userReportGenerator.GenerateUserReport(users);
                Console.WriteLine(report);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating report: {ex.Message}");
            }
        }


    }

   
}
