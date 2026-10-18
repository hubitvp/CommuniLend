namespace CommuniLend.Web.ViewModels.Tool
{
    public class DeleteViewModel
    {
        public required int ToolId { get; set; }

        public required string Name { get; set; }

        public required string CategoryName { get; set; }


        public required string OwnerFullName { get; set; }


        public required bool IsDeletable { get; set; }

    }
}
