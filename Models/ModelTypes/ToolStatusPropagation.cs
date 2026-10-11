using CommuniLend.Models.Enums;

namespace CommuniLend.Models.ModelTypes
{

    public class ToolStatusFormat
    {

        public string StatusAsString { get; set; } = null!;

        public string StatusCssClass { get; set; } = null!;

    }



    public class ToolStatusPropagation
    {

        private readonly Dictionary<ToolStatus, ToolStatusFormat> map = new()
        {
            { ToolStatus.Available, new (){ StatusAsString="Available",  StatusCssClass="bg-success"} },
            { ToolStatus.OnLoan, new (){ StatusAsString="On Loan",  StatusCssClass="bg-warning text-dark"} },
            { ToolStatus.Locked, new (){ StatusAsString="Locked",  StatusCssClass="bg-dark text-white"} },
            { ToolStatus.OverDue, new (){ StatusAsString="OverDue",  StatusCssClass="bg-danger"} },
        };


        public required ToolStatus Status { get; set; }


        public ToolStatusFormat StatusFormat => map[Status];

    }
}
