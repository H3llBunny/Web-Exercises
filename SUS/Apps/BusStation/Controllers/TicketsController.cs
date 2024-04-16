using BusStation.Services;
using BusStation.ViewModels.Tickets;
using SUS.HTTP;
using SUS.MvcFramework;
using System.Net.Http.Headers;

namespace BusStation.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ITicketsService ticketsService;
        private readonly IDestinationsService destinationsService;

        public TicketsController(ITicketsService ticketsService, IDestinationsService destinationsService)
        {
            this.ticketsService = ticketsService;
            this.destinationsService = destinationsService;
        }
        public HttpResponse Create()
        {
            if (!this.IsUserSignedIn())
            {
                return Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(int destinationId, TicketInputModel ticket)
        {
            if (!this.IsUserSignedIn())
            {
                return Redirect("/Users/Login");
            }

            if (!this.destinationsService.DoesDestinationExist(destinationId))
            {
                return this.Error("Destination does not exist");
            }

            if (ticket.Price == null || ticket.Price < 10 || ticket.Price > 90)
            {
                return this.Error("Price should be between 10 and 90 euros.");
            }

            if (ticket.TicketsCount == null || ticket.TicketsCount < 1 || ticket.TicketsCount > 10)
            {
                return this.Error("Tickets should be between 1 and 10");
            }

            this.ticketsService.AddTicketsToDestination(destinationId, ticket.Price, ticket.TicketsCount);

            return this.Redirect("/Destinations/All");
        }

        public HttpResponse Reserve(int destinationId)
        {
            if (!this.IsUserSignedIn())
            {
                return Redirect("/Users/Login");
            }

            if (!this.destinationsService.DoesDestinationExist(destinationId))
            {
                return this.Error("Destination does not exist");
            }

            if (!this.ticketsService.AnyTicketsLeft(destinationId))
            {
                return this.Error("No tickets available.");
            }

            var userId = this.GetUserId();
            this.ticketsService.BookATicket(destinationId, userId);

            return this.Redirect("/Tickets/MyTickets");
        }

        public HttpResponse MyTickets()
        {
            if (!this.IsUserSignedIn())
            {
                return Redirect("/Users/Login");
            }

            var ticketsViewModel = this.ticketsService.GetUserTickets(this.GetUserId());
           
            return this.View(ticketsViewModel);
        }
    }
}
