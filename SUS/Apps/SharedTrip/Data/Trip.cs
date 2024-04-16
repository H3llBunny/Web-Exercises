
using System.ComponentModel.DataAnnotations;

namespace SharedTrip.Data
{
    public class Trip
    {
        public Trip()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Users = new HashSet<UserTrip>();
        }

        public string Id { get; set; }

        [Required]
        public string StartPoint { get; set; }

        [Required]
        public string EndPoint { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        [MaxLength(6)]
        public int Seats { get; set; }

        [Required]
        [MaxLength(80)]
        public string Description { get; set; }

        public string ImagePath { get; set; }

        public virtual ICollection<UserTrip> Users { get; set; }

    }
}
