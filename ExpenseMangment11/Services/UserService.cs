using System.Threading.Tasks;
using ExpenseMangement11.Models;

using Microsoft.AspNetCore.Identity;


namespace ExpenseMangement11.Services
{
    public interface IUserService
    {

        //IEnumerable<User> GetUsers();
       // User GetUser(int userId);
        // void AddUser(User user);
        //void UpdateUser(User user);
       // void DeleteUser(int userId);

        


        // Task<User> Authenticate(string username, string password);
        // Task<User> Register(User user);
        //Task Logout();

        Task<IdentityResult> RegisterAsync(RegisterModel model);
        Task<string> LoginAsync(LoginModel model);
        Task<string> RefreshTokenAsync(string token);
        Task LogoutAsync();
    }



    public class UserService : IUserService 
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Implementation of authentication, registration, and logout methods...

        public IEnumerable<User> GetUsers()
        {
            try
            {
                return _context.Users.ToList();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new ServiceException("Error occurred while fetching users.", ex);
            }
        }
        public User GetUser(int userId)
        {
            try
            {
                return _context.Users.Find(userId);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new ServiceException($"Error occurred while fetching user with ID {userId}.", ex);
            }
        }
        public void AddUser(User user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new ServiceException("Error occurred while adding a new user.", ex);
            }
        }
        public void UpdateUser(User user)
        {
            try
            {
                _context.Users.Update(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new ServiceException($"Error occurred while updating user with ID {user.UserID}.", ex);
            }
        }
        public void DeleteUser(int userId)
        {
            try
            {
                var user = _context.Users.Find(userId);
                if (user != null)
                {
                    _context.Users.Remove(user);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new ServiceException($"Error occurred while deleting user with ID {userId}.", ex);
            }
        }

        public Task<IdentityResult> RegisterAsync(RegisterModel model)
        {
            throw new NotImplementedException();
        }

        public Task<string> LoginAsync(LoginModel model)
        {
            throw new NotImplementedException();
        }

        public Task<string> RefreshTokenAsync(string token)
        {
            throw new NotImplementedException();
        }

        public Task LogoutAsync()
        {
            throw new NotImplementedException();
        }
    }
     public class ServiceException : Exception
    {
        public ServiceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
