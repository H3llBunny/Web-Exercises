
using System.ComponentModel.DataAnnotations;

namespace BusStation.Data
{
	public class User
	{
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Tickets = new HashSet<Ticket>();
        }

        public string Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Username { get; set; }

		[Required]
		[MaxLength(60)]
		public string Email { get; set; }

        public string Password { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
