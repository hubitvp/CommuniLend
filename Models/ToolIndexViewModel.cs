using CommuniLend.Models.Enums;

namespace CommuniLend.Models
{
    public class ToolIndexViewModel
    {


        public required int ToolId { get; set; }

        public required string Name { get; set; }

        public required string OwnerFullName { get; set; }

        public required string? ImageUrl { get; set; }


        public required ToolStatus Status { get; set; }

        public string StatusAsText => Status switch
        {
            ToolStatus.Available => "Available",
            ToolStatus.OnLoan => "On Loan",
            ToolStatus.Locked => "Locked",
            _ => "Unknown"
        };

        public string StatusCssClass => Status switch
        {
            ToolStatus.Available => "bg-success",
            ToolStatus.OnLoan => "bg-warning text-dark",
            ToolStatus.Locked => "bg-danger",
            _ => "bg-secondary"
        };


        public required string CategoryName { get; set; }

    }
}
