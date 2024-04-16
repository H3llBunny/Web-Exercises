
using System.ComponentModel.DataAnnotations;

namespace SharedTrip.Data
{
    public class User
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Trips = new HashSet<UserTrip>();
        }
        public string Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public virtual ICollection<UserTrip> Trips { get; set; }
    }
}
