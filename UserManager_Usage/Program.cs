using UsersManagement.DataAccess;
using UsersManagement.UserManager;
using UsersManagement.UserModel;
using UsersManagement.UserReportRepository;
using UsersManagement.UserRepository;

IUserRepository userRepository = new InMemoryUserRepository();
IUserReportGenerator reportGenerator = new UserReportGenerator();
UserManager userManager = new UserManager(userRepository, reportGenerator);

// Add users
var newUser = new User("John Doe", "john.doe@example.com");
var newUser2 = new User("Jane Smith", "jane.smith@example.com");
userManager.AddUser(newUser);
userManager.AddUser(newUser2);



// Generate report after update
Console.WriteLine("\nReport After Update:");
userManager.GenerateAndPrintReport();

// Remove a user
userManager.RemoveUser("joh.doe@example.com");


// Generate final report
Console.WriteLine("\nFinal Report:");
userManager.GenerateAndPrintReport();

Console.ReadKey();