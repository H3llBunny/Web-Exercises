
using SMS.Data;
using System.ComponentModel.DataAnnotations;

namespace SMS.ViewModels.Products
{
	public class ProducViewModel
	{
		public string Id { get; set; }

		public string Name { get; set; }

		public decimal Price { get; set; }

		public string CartId { get; set; }
	}
}
