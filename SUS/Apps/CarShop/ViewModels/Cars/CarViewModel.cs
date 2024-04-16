using CarShop.Data;
using CarShop.ViewModels.Issues;
using System.ComponentModel.DataAnnotations;

namespace CarShop.ViewModels.Cars
{
    public class CarViewModel
    {
        public string Id { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public string PictureUrl { get; set; }

        public string PlateNumber { get; set; }

        public string OwnerId { get; set; }

        public virtual ICollection<IssueViewModel> Issues { get; set; }
    }
}
