
using SharedTrip.ViewModels.Trips;

namespace SharedTrip.Services
{
    public interface ITripsService
    {
        void AddTrip(TripViewModel input);

		ICollection<TripViewModel> GetAll();

		TripViewModel GetByTripId(string tripId);

		bool AddUserToTrip(string userId, string tripId);

		bool IsUserAlreadyJoinedTrip(string userId, string tripId);
	}
}
