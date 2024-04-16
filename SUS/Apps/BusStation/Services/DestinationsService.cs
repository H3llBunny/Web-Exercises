using BusStation.Data;
using BusStation.ViewModels.Destinations;
using Microsoft.EntityFrameworkCore;

namespace BusStation.Services
{
    public class DestinationsService : IDestinationsService
    {
        private readonly ApplicationDbContext db;

        public DestinationsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public ICollection<DestinationViewModel> GetAllDestionations()
        {
            return this.db.Destinations.Select(x => new DestinationViewModel
            {
                Id = x.Id,
                ImageUrl = x.ImageUrl,
                Tickets = x.Tickets,
                DestinationName = x.DestinationName,
                Origin = x.Origin,
                Date = x.Date.Date.ToString("d/MM/yyyy"),
                Time = x.Time.ToString("h:mm tt")
            }).ToList();
        }

        public void AddNewDestinations(string destinationName, string origin, DateTime date, TimeSpan time, string imageUrl)
        {
            var destination = new Destination
            {
                DestinationName = destinationName,
                Origin = origin,
                Date = date.Date,
                Time = DateTime.MinValue.Date + time,
                ImageUrl = imageUrl
            };

            this.db.Destinations.Add(destination);
            this.db.SaveChanges();
        }

        public bool DoesDestinationExist(int destinationId)
        {
            return this.db.Destinations.Any(x => x.Id == destinationId);
        }
    }
}
