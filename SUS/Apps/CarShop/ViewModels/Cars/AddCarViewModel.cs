
using CarShop.Data;
using System.ComponentModel.DataAnnotations;

namespace CarShop.ViewModels.Cars
{
    public class AddCarViewModel
    {
        public string Id { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public string ImageUrl { get; set; }

        public string PlateNumber { get; set; }

        public string OwnerId { get; set; }
    }
}
