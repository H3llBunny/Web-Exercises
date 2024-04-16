
using System.ComponentModel.DataAnnotations;

namespace SMS.Data
{
    public class Cart
    {
        public Cart()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Products = new HashSet<Product>();
        }

        public string Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
