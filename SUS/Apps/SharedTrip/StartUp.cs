using Microsoft.EntityFrameworkCore;
using SharedTrip.Data;
using SharedTrip.Services;
using SUS.HTTP;
using SUS.MvcFramework;

namespace SharedTrip
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
            serviceCollection.Add<ITripsService, TripsService>();
        }
    }
}
