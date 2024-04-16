
using BusStation.ViewModels.Destinations;

namespace BusStation.ViewModels.Tickets
{
    public class TicketViewModel
    {
        public decimal Price { get; set; }

        public DestinationViewModel TicketDestination { get; set; }
    }
}
