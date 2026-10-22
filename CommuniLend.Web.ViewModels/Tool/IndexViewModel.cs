using CommuniLend.Web.ViewModels.Category;
using CommuniLend.Web.ViewModels.Enums;


namespace CommuniLend.Web.ViewModels.Tool
{
    public class ToolIndexViewModel
    {
        public required int ToolId { get; set; }
        public required string ToolName { get; set; }


        public required string? ToolImageUrl { get; set; }


        public required ToolAvailabilityStatus AvailabilityStatus { get; set; }

        public required string OwnerId { get; set; }


        public required string OwnerFullName { get; set; }


        public required string CategoryName { get; set; }

    }


    public class IndexViewModel
    {

        public required IEnumerable<ToolIndexViewModel> Tools { get; set; }


        public required IEnumerable<CategoryViewModel> Categories { get; set; }

        public string? SearchString { get; set; }
        public int? CategoryId { get; set; }
    }


}
