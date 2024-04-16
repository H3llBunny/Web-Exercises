using System.ComponentModel.DataAnnotations;

namespace CarShop.Data
{
    public class Issue
    {
        public Issue()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        [Required]
        public string Desctiption { get; set; }

        [Required]
        public bool IsFixed { get; set; }

        public string CarId { get; set; }

        public virtual Car Car { get; set; }
    }
}
