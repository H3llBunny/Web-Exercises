using SUS.MvcFramework;

namespace SMS
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await Host.CreateHostAsync(new StartUp());
        }
    }
}
