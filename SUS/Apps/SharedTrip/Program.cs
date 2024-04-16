using SUS.MvcFramework;

namespace SharedTrip
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            await Host.CreateHostAsync(new StartUp());
        }
    }
}
