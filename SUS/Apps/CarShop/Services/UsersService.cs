using CarShop.Data;
using System.Security.Cryptography;
using System.Text;

namespace CarShop.Services
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext db;

        public UsersService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public string GetUserId(string username, string password)
        {
            var user = this.db.Users.FirstOrDefault(x => x.Username == username);

            if (user?.Password != ComputeHash(password))
            {
                return null;
            }

            return user.Id;
        }

        public bool IsUsernameAvailable(string username)
        {
            return !this.db.Users.Any(x => x.Username == username);
        }

        public bool IsEmailAvailable(string email)
        {
            return !this.db.Users.Any(x => x.Email == email);
        }

        public void CreateUser(string username, string email, string password, string userType)
        {
            bool isMechanic = false;

            if (userType == "Mechanic")
            {
                isMechanic = true;
            }

            var user = new User
            {
                Username = username,
                Email = email,
                Password = ComputeHash(password),
                IsMechanic = isMechanic
            };

            this.db.Users.Add(user);
            this.db.SaveChanges();
        }

        public bool IsUserMechanic(string userId)
        {
            return this.db.Users.Any(x => x.Id == userId && x.IsMechanic == true);
        }

        private static string ComputeHash(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            using (var hash = SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);
                // Convert to text
                // StringBuilder Capacity is 128, because 512 bits / 8 bits in byte * 2 symbols for byte 
                var hashedInputStringBuilder = new StringBuilder(128);
                foreach (var b in hashedInputBytes)
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                return hashedInputStringBuilder.ToString();
            }
        }
    }
}
