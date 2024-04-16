
using SMS.ViewModels.Products;

namespace SMS.ViewModels.Users
{
    public class HomePageViewModel
    {
        public HomePageViewModel()
        {
            this.AllProducts = new HashSet<ProducViewModel>();
        }

        public string Username { get; set; }

        public ICollection<ProducViewModel> AllProducts { get; set; }
    }
}
