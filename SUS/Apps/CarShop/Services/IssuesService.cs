
using CarShop.Data;
using CarShop.ViewModels.Cars;
using CarShop.ViewModels.Issues;
using Microsoft.EntityFrameworkCore;

namespace CarShop.Services
{
    public class IssuesService : IIssuesService
    {
        private readonly ApplicationDbContext db;

        public IssuesService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public CarViewModel GetCarById(string carId)
        {
            return this.db.Cars.Where(x => x.Id == carId)
                .Select(x => new CarViewModel
                {
                    Id = x.Id,
                    Model = x.Model,
                    PlateNumber = x.PlateNumber,
                    OwnerId = x.OwnerId,
                    PictureUrl = x.PictureUrl,
                    Issues = x.Issues.Select(x => new IssueViewModel
                    {
                        Id = x.Id,
                        Desctiption = x.Desctiption,
                        IsFixed = x.IsFixed,
                        CarId = x.CarId
                    }).ToList()
                }).FirstOrDefault();
        }

        public void AddNewIssue(string carId, string description)
        {
            var car = this.db.Cars.FirstOrDefault(x => x.Id == carId);
            car.Issues.Add(new Issue
            {
                Desctiption = description,
                IsFixed = false,
                CarId = carId
            });
            this.db.SaveChanges();
        }

        public void FixIssue(string issueId, string carId)
        {
            var car = this.db.Cars.Where(x => x.Id == carId)
                .Include(x => x.Issues)
                .FirstOrDefault();
            var issue = car.Issues.FirstOrDefault(x => x.Id == issueId);
            issue.IsFixed = true;
            this.db.SaveChanges();
        }

        public void DeleteIssue(string issueId, string carId)
        {
            var car = this.db.Cars.Where(x => x.Id == carId)
                .Include(x => x.Issues)
                .FirstOrDefault();
            var issue = car.Issues.FirstOrDefault(x => x.Id == issueId);
            car.Issues.Remove(issue);
            this.db.SaveChanges();
        }
    }
}
