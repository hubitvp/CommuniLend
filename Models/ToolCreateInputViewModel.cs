using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CommuniLend.Models
{

    public class InputDataModel
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string Description { get; set; } = null!;


        [Required]
        public int ConditionId { get; set; }

        [Required]
        [Display(Name = "Replacement Value")]
        public string ReplacementValue { get; set; } = null!;

        public string? ImageUrl { get; set; }

    }

    public class OutputDataModel
    {

        public IEnumerable<SelectListItem> Categories { get; set; } = null!;

        public IEnumerable<SelectListItem> ConditionOptions { get; set; } = null!;
    }



    public class ToolCreateInputViewModel
    {
        [Required]
        public InputDataModel InputDataModel { get; set; } = null!;

        public OutputDataModel? OutputDataModel { get; set; }

    }
}
