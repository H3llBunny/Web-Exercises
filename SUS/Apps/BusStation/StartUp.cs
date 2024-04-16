
using BusStation.Data;
using BusStation.Services;
using Microsoft.EntityFrameworkCore;
using SUS.HTTP;
using SUS.MvcFramework;

namespace BusStation
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
            serviceCollection.Add<IDestinationsService, DestinationsService>();
            serviceCollection.Add<ITicketsService, TicketsService>();

        }
    }
}
