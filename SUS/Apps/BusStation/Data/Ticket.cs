
namespace BusStation.Data
{
	public class Ticket
	{
        public int Id { get; set; }

        public decimal Price { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public int DestinationId { get; set; }
        public virtual Destination Destination { get; set; }
    }
}
