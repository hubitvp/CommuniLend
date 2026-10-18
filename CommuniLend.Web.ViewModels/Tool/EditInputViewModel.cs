using CommuniLend.Data.Models.Constraints;
using CommuniLend.Web.ViewModels.Category;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommuniLend.Web.ViewModels.Tool
{
    public class EditInputViewModel
    {
        public required int ToolId { get; set; }


        [Required]
        [StringLength(ValidationConstants.Tool.NameMaxLength)]
        public required string ToolName { get; set; }


        [Required]
        [StringLength(ValidationConstants.Tool.DescriptionMaxLength)]
        public required string Description { get; set; }

        [Required]
        [Display(Name = "Condition")]
        public int ConditionId { get; set; }


        [Required]
        [Display(Name = "Replacement Value in € EUR")]
        [RegularExpression(
        @"^\d{1,6}([.]\d{1,2})?$",
        ErrorMessage = "Price must be between 0.01 and 999999.99.")]
        public string ReplacementValue { get; set; } = null!;


        public string? ToolImageUrl { get; set; }


        public required string OwnerFullName { get; set; }
        public required string AddedOn { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }



        public required IEnumerable<CategoryViewModel> Categories { get; set; }
        public required IEnumerable<ConditionOptionModel> ConditionOptions { get; set; }
    }
}
