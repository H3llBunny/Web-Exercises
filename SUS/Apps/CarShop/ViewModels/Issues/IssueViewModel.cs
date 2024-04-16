using CarShop.Data;
using System.ComponentModel.DataAnnotations;

namespace CarShop.ViewModels.Issues
{
    public class IssueViewModel
    {
        public string Id { get; set; }

        public string Desctiption { get; set; }

        public bool IsFixed { get; set; }

        public string IsFixedString => this.IsFixed ? "Yes" : "Not yet";

        public string CarId { get; set; }
    }
}
