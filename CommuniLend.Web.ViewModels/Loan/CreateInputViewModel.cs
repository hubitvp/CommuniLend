using CommuniLend.Data.Models.Constraints;
using CommuniLend.Web.ViewModels.Tool;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace CommuniLend.Web.ViewModels.Loan
{
    public class CreateInputViewModel : IValidatableObject
    {




        public required ToolViewModel? ToolViewModel { get; set; }



        [Required]
        public string LoanDate { get; set; } = null!;

        [Required]
        public string DueDate { get; set; } = null!;


        [StringLength(ValidationConstants.Loan.NotesMaxLength)]
        public string? Notes { get; set; }



        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            bool loanDateParsed = DateOnly.TryParse(LoanDate, CultureInfo.InvariantCulture, out var loanDate);

            if (false == loanDateParsed)
            {
                yield return new ValidationResult(
                    "Loan date is not valid",
                    new[] { nameof(LoanDate) });
            }

            bool dueDateParsed = DateOnly.TryParse(DueDate, CultureInfo.InvariantCulture, out var dueDate);

            if (false == dueDateParsed)
            {
                yield return new ValidationResult(
                    "Due date is not valid",
                    new[] { nameof(DueDate) });
            }


            if (loanDate <= DateOnly.FromDateTime(DateTime.UtcNow))
            {
                yield return new ValidationResult(
                    "Loan date should be in the future. Use tomorrow or further date",
                    new[] { nameof(LoanDate) });
            }

            if (dueDate <= loanDate)
            {
                yield return new ValidationResult(
                    "Due date should at least one day after loan date",
                    new[] { nameof(DueDate) });
            }


        }
    }
}
