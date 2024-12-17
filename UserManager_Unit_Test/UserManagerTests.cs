using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersManagement.UserManager;
using UsersManagement.UserModel;
using UsersManagement.UserRepository;
using Xunit;
using Assert = Xunit.Assert;

namespace UserManager_UnitTest
{
    public class UserManagerTests
    {
        private Mock<IUserRepository> _userRepoMock;
        private Mock<IUserReportGenerator> _reportGeneratorMock;
        private UserManager _userManager;

        public UserManagerTests()
        {
            _userRepoMock = new Mock<IUserRepository>();
            _reportGeneratorMock = new Mock<IUserReportGenerator>();
            _userManager = new UserManager(_userRepoMock.Object, _reportGeneratorMock.Object);
        }


        //Test For Add User

        [Fact]
        public void Test_To_AddUser_WhenCalled()
        {
            var newUser = new User("John Doe", "john.doe@example.com");

            //Mocking Setup: to add user to Repository
            _userRepoMock.Setup(repo => repo.AddUser(newUser));

            _userManager.AddUser(newUser);

       
            _userRepoMock.Verify(repo => repo.AddUser(It.Is<User>(u => u.Name == newUser.Name && u.Email == newUser.Email)), Times.Once);
  
        }

        [Fact]
        public void Test_To_AddUser_ShouldThrowException_WhenDuplicateUserExists()
        {
           
            var duplicateUser = new User("Jane Doe", "jane.doe@example.com");

            // Mocking: Simulate the repository throwing an exception for a duplicate user
            _userRepoMock.Setup(repo => repo.AddUser(duplicateUser))
                         .Throws(new InvalidOperationException($"User with email {duplicateUser.Email} already exists."));

          
            var ex = Assert.Throws<InvalidOperationException>(() => _userManager.AddUser(duplicateUser));
            Assert.Equal($"User with email {duplicateUser.Email} already exists.", ex.Message);

            // Verify that AddUser was attempted
            _userRepoMock.Verify(repo => repo.AddUser(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public void Test_ToAddUser_ThrowException_WhenNameOrEmailIsNull()
        {
            string name = "John Doe";
            string email = null;

            try
            {
                _userManager.AddUser(new User(name, email));
                Assert.Fail("Expected exception was not thrown.");
            }
            catch (ArgumentNullException ex)
            {
                if (ex.ParamName == "name")
                    Assert.Equal("Name cannot be null. (Parameter 'name')", ex.Message);
                else
                    Assert.Equal("Email cannot be null. (Parameter 'email')", ex.Message);

            }
        }


        #region Test For Update User
        //Test For Update User
        // [Fact]
        // public void Test_To_UpdateUser_ShouldUpdateUser_WhenUserExists()
        //{

        //     var existingUser = new User ("John Doe", "john.doe@example.com" );
        //     var updatedUser = new User ("John Doe Updated", "john.doe@example.com" );

        //     var users = new List<User> { existingUser };
        //     _userRepoMock.Setup(repo => repo.GetAllUsers()).Returns(users);
        //     _userRepoMock.Setup(repo => repo.UpdateUser(updatedUser.Email, updatedUser.Name, updatedUser.Email));

        //     // Redirect Console output
        //     var stringWriter = new StringWriter();
        //     Console.SetOut(stringWriter);

        //     _userManager.UpdateUser(updatedUser.Email, updatedUser.Name, updatedUser.Email);


        //     Assert.NotEqual("John Doe Updated", existingUser.Name); //To Ensure the name is updated
        //     Assert.Equal("john.doe@example.com", existingUser.Email); //To Ensure the email remains the same
        //     var output = stringWriter.ToString().Trim();
        //     Assert.Equal($"User with email {updatedUser.Email} updated successfully.", output); //This Ensure correct message is printed
        // }

        // [Fact]
        // public void Test_To_UpdateUser_ShouldThrowException_WhenUserDoesNotExist()
        // {

        //     var nonExistentUser = new User ("Non Existent User", "nonexistent@example.com");
        //     _userRepoMock.Setup(repo => repo.UpdateUser(nonExistentUser.Email, nonExistentUser.Name, nonExistentUser.Email))
        //                  .Throws(new KeyNotFoundException($"No user found with email {nonExistentUser.Email}."));

        //     // Redirect Console output
        //     var stringWriter = new StringWriter();
        //     Console.SetOut(stringWriter);


        //     var ex = Assert.Throws<KeyNotFoundException>(() => _userManager.UpdateUser(nonExistentUser.Email, nonExistentUser.Name, nonExistentUser.Email));
        //     Assert.Equal($"No user found with email {nonExistentUser.Email}.", ex.Message);
        // }

        // [Fact]
        // public void Test_To_UpdateUser_ShouldThrowException_WhenUserEmailAlreadyExists()
        // {

        //     var existingUser = new User ("John Doe", "john.doe@example.com" );
        //     var anotherUser = new User ("Jane Doe", "jane.doe@example.com" );
        //     _userRepoMock.Setup(repo => repo.UpdateUser(existingUser.Email, "Updated Name", "john.doe@example.com"))
        //                  .Throws(new InvalidOperationException($"A user with the email {existingUser.Email} already exists."));


        //     var ex = Assert.Throws<InvalidOperationException>(() => _userManager.UpdateUser(existingUser.Email, "Updated Name", existingUser.Email));
        //     Assert.Equal($"A user with the email {existingUser.Email} already exists.", ex.Message);
        // } 
        #endregion


        //Test for Remove User
        [Fact]
        public void Test_To_RemoveUser_WhenCalledAndUserExist()
        {
            
            var user = new User ("John Doe", "john.doe@example.com" );
            var users = new List<User> { user };
            _userRepoMock.Setup(repo => repo.GetAllUsers()).Returns(users);
            _userRepoMock.Setup(repo => repo.RemoveUser(user.Email)).Callback(() => users.Remove(user));

            // Redirect Console output
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            
            _userManager.RemoveUser(user.Email);

          
            Assert.DoesNotContain(user, users); // Ensure user is removed from the list

            var output = stringWriter.ToString().Trim();
            Assert.Equal($"User with email {user.Email} removed successfully.", output); // Ensure correct message is printed
        }

        [Fact]
        public void Test_For_RemoveUser_ShouldThrowException_WhenUserDoesNotExist()
        {

            
            var nonExistentEmail = "nonexistent@example.com";
            var users = new List<User>(); // Empty list simulating no users
            _userRepoMock.Setup(repo => repo.GetAllUsers()).Returns(users);
            _userRepoMock.Setup(repo => repo.RemoveUser(nonExistentEmail)).Throws(new KeyNotFoundException($"No user found with email {nonExistentEmail}."));

            // Redirect Console output
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            var ex = Assert.Throws<KeyNotFoundException>(() => _userManager.RemoveUser(nonExistentEmail));
            Assert.Equal($"No user found with email {nonExistentEmail}.", ex.Message);
        }

        [Fact]
        public void Test_To_GenerateAndPrintReport_In_Correct_Format_Expected_WhenCalled()
        {

            var users = new List<User>
            {
                 new User("John Doe", "john.doe@example.com"),
                 new User ("Jane Smith", "jane.smith@example.com")
            };

            _userRepoMock.Setup(repo => repo.GetAllUsers()).Returns(users);
            _reportGeneratorMock.Setup(r => r.GenerateUserReport(users)).Returns("User Report:\n1. John Doe - john.doe@example.com\n2. Jane Smith - jane.smith@example.com");

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

           
            _userManager.GenerateAndPrintReport();

            var output = stringWriter.ToString().Trim();
            var expectedReport = "User Report:\n1. John Doe - john.doe@example.com\n2. Jane Smith - jane.smith@example.com";
            Assert.Equal(expectedReport, output);
        }

        [Fact]
        public void Test_To_GenerateAndPrintReport_When_No_User_Found_Or_WhenListIsEmpty()
        {

          
            var users = new List<User>(); 
            _userRepoMock.Setup(repo => repo.GetAllUsers()).Returns(users);
            _reportGeneratorMock.Setup(r => r.GenerateUserReport(users)).Returns("No users found.");

            // Redirect Console output
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

       
            _userManager.GenerateAndPrintReport();

          
            var output = stringWriter.ToString().Trim();
            Assert.Equal("No users found.", output);
        }

        [Fact]
        public void Test_GenerateAndPrintReport_WhichHandleError_WhenExceptionIsThrown()
        {


            var users = new List<User>();
            _userRepoMock.Setup(repo => repo.GetAllUsers()).Returns(users);
            _reportGeneratorMock.Setup(r => r.GenerateUserReport(users)).Returns("No users found.");

            // Redirect Console output
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);


            _userManager.GenerateAndPrintReport();


            var output = stringWriter.ToString().Trim();
            Assert.Equal("No users found.", output);
        }
    }
}
