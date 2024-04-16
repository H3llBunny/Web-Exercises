using Microsoft.EntityFrameworkCore;
using SMS.Data;
using SMS.Services;
using SUS.HTTP;
using SUS.MvcFramework;

namespace SMS
{
    public class StartUp : IMvcApplication
    {
        public void Configure(List<Route> routeTable)
        {
            new ApplicationDbContext().Database.Migrate();
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUsersService, UsersService>();
			serviceCollection.Add<IProductsService, ProductsService>();
            serviceCollection.Add<ICartsService, CartsService>();
		}
	}
}
