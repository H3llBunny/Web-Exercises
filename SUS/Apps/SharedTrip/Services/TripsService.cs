
using SharedTrip.Data;
using SharedTrip.ViewModels.Trips;

namespace SharedTrip.Services
{
	public class TripsService : ITripsService
	{
		private readonly ApplicationDbContext db;

		public TripsService(ApplicationDbContext db)
		{
			this.db = db;
		}

		public void AddTrip(TripViewModel input)
		{
			var trip = new Trip
			{
				StartPoint = input.StartPoint,
				EndPoint = input.EndPoint,
				DepartureTime = input.DepartureTime,
				ImagePath = input.ImagePath,
				Seats = input.Seats,
				Description = input.Description
			};

			this.db.Trips.Add(trip);
			this.db.SaveChanges();
		}

		public ICollection<TripViewModel> GetAll()
		{
			return this.db.Trips.Select(x => new TripViewModel
			{
				StartPoint = x.StartPoint,
				EndPoint = x.EndPoint,
				DepartureTime = x.DepartureTime,
				Seats = x.Seats,
				Id = x.Id,
				ImagePath = x.ImagePath
			}).ToList();
		}

		public TripViewModel GetByTripId(string tripId)
		{
			return this.db.Trips.Where(x => x.Id == tripId)
				 .Select(x => new TripViewModel
				 {
					 StartPoint = x.StartPoint,
					 EndPoint = x.EndPoint,
					 DepartureTime = x.DepartureTime,
					 Seats = x.Seats,
					 Description = x.Description,
					 Id = x.Id,
					 ImagePath = x.ImagePath
				 }).FirstOrDefault();
		}

		public bool IsUserAlreadyJoinedTrip(string userId, string tripId)
		{
			return this.db.UserTrips.Any(x => x.UserId == userId && x.TripId == tripId);
		}

		public bool AddUserToTrip(string userId, string tripId)
		{
			this.db.UserTrips.Add(new UserTrip { UserId = userId, TripId = tripId });
			var trip = this.db.Trips.FirstOrDefault(x => x.Id == tripId);
			if (trip.Seats > 0)
			{
				trip.Seats -= 1;
				db.SaveChanges();
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
