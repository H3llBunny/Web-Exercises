
using BusStation.ViewModels.Destinations;
using BusStation.ViewModels.Tickets;

namespace BusStation.Services
{
    public interface ITicketsService
    {
        void AddTicketsToDestination(int destinationId, decimal price, int ticketCount);

        bool AnyTicketsLeft(int destinationId);

        void BookATicket(int destinationId, string userId);

        ICollection<TicketViewModel> GetUserTickets(string userId);
    }
}
