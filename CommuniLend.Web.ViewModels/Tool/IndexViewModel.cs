using CommuniLend.Web.ViewModels.Enums;


namespace CommuniLend.Web.ViewModels.Tool
{
    public class IndexViewModel 
    {

        public required int ToolId { get; set; }

        public required string ToolName { get; set; }


        public required ToolStatus ToolStatus { get; set; }

        public required int OwnerId { get; set; }


        public required string OwnerFullName { get; set; }

        public required string? ToolImageUrl { get; set; }

        public required string CategoryName { get; set; }

    }
}
