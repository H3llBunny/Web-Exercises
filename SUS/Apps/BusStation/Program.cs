using Microsoft.CodeAnalysis.CSharp.Syntax;
using SUS.MvcFramework;

namespace BusStation
{
	internal class Program
	{
		static async Task Main(string[] args)
		{
			await Host.CreateHostAsync(new StartUp());
		}
	}
}
