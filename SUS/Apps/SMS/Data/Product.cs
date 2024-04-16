
using System.ComponentModel.DataAnnotations;

namespace SMS.Data
{
    public class Product
    {
        public Product()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string CartId { get; set; }
        public virtual Cart Cart { get; set; }
    }
}
