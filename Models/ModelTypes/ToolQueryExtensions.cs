using CommuniLend.Data.Models;
using CommuniLend.Models.Enums;

namespace CommuniLend.Models.ModelTypes
{
    public record ToolWithStatus(Tool Tool, ToolStatus Status);

    public static class ToolQueryExtensions
    {
        public static IQueryable<ToolWithStatus> WithStatus(
            this IQueryable<Tool> tools, DateOnly? today = null)
        {
            var date = today ?? DateOnly.FromDateTime(DateTime.UtcNow);

            return tools.Select(tool => new ToolWithStatus(
                tool,

                tool.OpenLoan == null
                ? tool.Owner.IsActive
                    ? ToolStatus.Available
                    : ToolStatus.Locked
                : tool.OpenLoan.DueDate >= date
                    ? ToolStatus.OnLoan
                    : ToolStatus.OverDue));
        }
    }
}
