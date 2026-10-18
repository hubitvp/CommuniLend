using CommuniLend.Data.Models;
using CommuniLend.Web.ViewModels.Enums;

namespace CommuniLend.Services.Extensions
{
    internal record ToolWithStatus(Tool Tool, ToolStatus Status);

    internal static class ToolQueryExtensions
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
