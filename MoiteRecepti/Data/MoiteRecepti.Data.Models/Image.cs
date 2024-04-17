﻿namespace MoiteRecepti.Data.Models
{
    using System;

    public class Image : BaseModel
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }

        public string Extension { get; set; }

        //// The contents of the image is in the file system.

        public string Id { get; set; }

        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }
    }
}
