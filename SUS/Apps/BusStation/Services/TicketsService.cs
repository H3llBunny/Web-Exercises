
using BusStation.Data;
using BusStation.ViewModels.Destinations;
using BusStation.ViewModels.Tickets;

namespace BusStation.Services
{
    public class TicketsService : ITicketsService
    {
        private readonly ApplicationDbContext db;

        public TicketsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void AddTicketsToDestination(int destinationId, decimal price, int ticketCount)
        {
            var destination = this.db.Destinations.FirstOrDefault(x => x.Id == destinationId);

            for (int i = 1; i <= ticketCount; i++)
            {
                var ticket = new Ticket { Price = price, DestinationId = destinationId };
                destination.Tickets.Add(ticket);
            }
            this.db.SaveChanges();
        }

        public bool AnyTicketsLeft(int destinationId)
        {
            return this.db.Tickets.Where(x => x.DestinationId == destinationId).Any(x => x.UserId == null);
        }

        public void BookATicket(int destinationId, string userId)
        {
            var ticket = this.db.Tickets.FirstOrDefault(x => x.DestinationId == destinationId);
            ticket.UserId = userId;

            this.db.SaveChanges();
        }

        public ICollection<TicketViewModel> GetUserTickets(string userId)
        {
            return this.db.Tickets
            .Where(x => x.UserId == userId)
            .Select(x => new TicketViewModel
            {
                Price = x.Price,
                TicketDestination = new DestinationViewModel
                {
                    Id = x.Id,
                    ImageUrl = x.Destination.ImageUrl,
                    Tickets = x.Destination.Tickets,
                    DestinationName = x.Destination.DestinationName,
                    Origin = x.Destination.Origin,
                    Date = x.Destination.Date.Date.ToString("d/MM/yyyy"),
                    Time = x.Destination.Time.ToString("h:mm tt")
                }
            }).ToList();
        }
    }
}
