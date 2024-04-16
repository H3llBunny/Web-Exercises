
using BusStation.Data;
using System.ComponentModel.DataAnnotations;

namespace BusStation.ViewModels.Destinations
{
    public class DestinationViewModel
    {
        public DestinationViewModel()
        {
            this.Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }

        public string DestinationName { get; set; }

        public string Origin { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public string ImageUrl { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
