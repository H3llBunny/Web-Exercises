namespace CarShop.Services
{
    public interface IUsersService
    {
        string GetUserId(string username, string password);

        bool IsUsernameAvailable(string username);

        bool IsEmailAvailable(string email);

        void CreateUser(string username, string email, string password, string userType);

        bool IsUserMechanic(string userId);
    }
}
