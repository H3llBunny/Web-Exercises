
using System.ComponentModel.DataAnnotations;

namespace BusStation.Data
{
	public class Destination
	{
        public Destination()
        {
			this.Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string DestinationName { get; set; }

		[Required]
		[MaxLength(50)]
		public string Origin { get; set; }

		[Required]
		[MaxLength(30)]
		[DataType(DataType.Date)]
		public DateTime Date { get; set; }

		[Required]
		[MaxLength(30)]
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }

		[Required]
        public string ImageUrl { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
