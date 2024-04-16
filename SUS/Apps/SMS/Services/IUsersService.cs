
namespace SMS.Services
{
    public interface IUsersService
    {
        bool IsUsernameAvailable(string username);

        bool IsEmailAvailable(string email);

        void CreateUser(string username, string email, string password);

        string GetUserId(string username, string password);

        string GetUsername(string userId);
    }
}
