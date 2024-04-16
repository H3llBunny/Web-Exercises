
using CarShop.ViewModels.Cars;
using CarShop.ViewModels.Issues;

namespace CarShop.Services
{
    public interface IIssuesService
    {
        CarViewModel GetCarById(string carId);

        void AddNewIssue(string carId, string description);

        void FixIssue(string issueId, string carId);

        void DeleteIssue(string issueId, string carId);
    }
}
