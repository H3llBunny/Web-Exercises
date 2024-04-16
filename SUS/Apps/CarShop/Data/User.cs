﻿using System.ComponentModel.DataAnnotations;

namespace CarShop.Data
{
    public class User
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Cars = new HashSet<Car>();
        }

        public string Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsMechanic { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
