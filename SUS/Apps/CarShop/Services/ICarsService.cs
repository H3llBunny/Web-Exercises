
using CarShop.Data;
using CarShop.ViewModels.Cars;

namespace CarShop.Services
{
    public interface ICarsService
    {
        void AddCar(AddCarViewModel car, string userId);

        IEnumerable<CarViewModel> GetAllCarsWithUnfixedIssues();

        IEnumerable<CarViewModel> GetClientCars(string userId);
    }
}
