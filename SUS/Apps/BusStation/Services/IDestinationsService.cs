using BusStation.ViewModels.Destinations;

namespace BusStation.Services
{
    public interface IDestinationsService
    {
        ICollection<DestinationViewModel> GetAllDestionations();

        void AddNewDestinations(string destinationName, string origin, DateTime date, TimeSpan time, string imageUrl);

        bool DoesDestinationExist(int destinationId);

        
    }
}
