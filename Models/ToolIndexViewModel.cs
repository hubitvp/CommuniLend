using CommuniLend.Models.ModelTypes;

namespace CommuniLend.Models
{
    public class ToolIndexViewModel : ToolStatusPropagation
    {

        public required int ToolId { get; set; }

        public required string Name { get; set; }


        public required int OwnerId { get; set; }


        public required string OwnerFullName { get; set; }

        public required string? ImageUrl { get; set; }



        public required string CategoryName { get; set; }

    }
}
