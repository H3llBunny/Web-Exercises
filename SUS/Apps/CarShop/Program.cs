using SUS.MvcFramework;

namespace CarShop
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await Host.CreateHostAsync(new StartUp());
        }
    }
}
