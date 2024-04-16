
namespace BusStation.Services
{
    public interface IUsersService
    {
        string GetUserId(string username, string password);

        bool IsEmailAvailable(string email);

        bool IsUsernameAvailable(string username);

        void CreateUser(string username, string email, string password);
    }
}
