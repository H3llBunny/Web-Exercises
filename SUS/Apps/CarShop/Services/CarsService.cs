
using CarShop.Data;
using CarShop.ViewModels.Cars;
using CarShop.ViewModels.Issues;

namespace CarShop.Services
{
    public class CarsService : ICarsService
    {
        private readonly ApplicationDbContext db;

        public CarsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void AddCar(AddCarViewModel car, string userId)
        {
            {
                var carToAdd = new Car
                {
                    Model = car.Model,
                    Year = car.Year,
                    PictureUrl = car.ImageUrl,
                    PlateNumber = car.PlateNumber,
                    OwnerId = userId
                };

                this.db.Cars.Add(carToAdd);
                this.db.SaveChanges();
            }
        }

        public IEnumerable<CarViewModel> GetAllCarsWithUnfixedIssues()
        {
            return this.db.Cars.Where(x => x.Issues.Any(x => x.IsFixed == false))
                .Select(x => new CarViewModel
            {
                Id = x.Id,
                Model = x.Model,
                Year = x.Year,
                PictureUrl = x.PictureUrl,
                PlateNumber = x.PlateNumber,
                OwnerId = x.OwnerId,
                Issues = x.Issues.Select(x => new IssueViewModel
                {
                    Id = x.Id,
                    Desctiption = x.Desctiption,
                    IsFixed = x.IsFixed,
                    CarId = x.CarId
                }).ToList()
            }).ToList();
        }

        public IEnumerable<CarViewModel> GetClientCars(string userId)
        {
            return this.db.Cars.Where(x => x.OwnerId == userId)
                .Select(x => new CarViewModel
                {
                    Id = x.Id,
                    Model = x.Model,
                    Year = x.Year,
                    PictureUrl = x.PictureUrl,
                    PlateNumber = x.PlateNumber,
                    OwnerId = x.OwnerId,
                    Issues = x.Issues.Select(x => new IssueViewModel
                    {
                        Id = x.Id,
                        Desctiption = x.Desctiption,
                        IsFixed = x.IsFixed,
                        CarId = x.CarId
                    }).ToList()
                }).ToList();
        }
    }
}
