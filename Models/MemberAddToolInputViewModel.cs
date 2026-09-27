using CommuniLend.DataConstraints;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CommuniLend.Models
{

    public class InputDataModel
    {
        [Required]
        [StringLength(ValidationConstants.Tool.NameMaxLength,
            MinimumLength = ValidationConstants.Tool.NameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(ValidationConstants.Tool.DescriptionMaxLength,
            MinimumLength = ValidationConstants.Tool.DescriptionMinLength)]
        public string Description { get; set; } = null!;


        [Required]
        [Display(Name = "Condition")]
        public int ConditionId { get; set; }

        [Required]
        [Display(Name = "Replacement Value")]
        [RegularExpression(
        @"^\d{1,6}([.]\d{1,2})?$",
        ErrorMessage = "Price must be between 0.01 and 999999.99.")]
        public string ReplacementValue { get; set; } = null!;


        public string? ImageUrl { get; set; }

    }

    public class OutputDataModel
    {
        public required int OwnerId { get; set; }

        public required string OwnerFullName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required string MemberSince { get; set; }

        public required bool IsActive { get; set; }

        public required string? Address { get; set; }


        public required IEnumerable<SelectListItem> Categories { get; set; }

        public required IEnumerable<SelectListItem> ConditionOptions { get; set; }
    }



    public class MemberAddToolInputViewModel
    {
        [Required]
        public InputDataModel InputDataModel { get; set; } = null!;

        public OutputDataModel? OutputDataModel { get; set; }

    }
}
