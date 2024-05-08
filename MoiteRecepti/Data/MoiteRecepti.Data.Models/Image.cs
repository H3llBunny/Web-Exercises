namespace MoiteRecepti.Data.Models
{
    using MoiteRecepti.Data.Common.Models;
    using System;

    public class Image : BaseModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }

        public string Extension { get; set; }

        //// The contents of the image is in the file system.

        public string RemoteImageUrl { get; set; }

        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }
    }
}
