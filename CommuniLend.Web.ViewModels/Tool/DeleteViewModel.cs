namespace CommuniLend.Web.ViewModels.Tool
{
    public class DeleteViewModel
    {
        public required int ToolId { get; set; }

        public required string ToolName { get; set; }

        public required string CategoryName { get; set; }
        public required string Condition { get; set; }
        public required string ReplacementValue { get; set; }

        public required string AddedOn { get; set; }

        public required string? ToolImageUrl { get; set; }

        public required string Description { get; set; }


        public required bool IsDeletable { get; set; }

    }
}
