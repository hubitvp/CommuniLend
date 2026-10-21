using CommuniLend.Data.Models.Constraints;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CommuniLend.Data.Models
{
    [Comment("Category that groups tools, e.g. Power Tools")]
    public class Category
    {

        [Key]
        [Comment("Primary key of the category")]
        public int Id { get; set; }


        [Required]
        [StringLength(ValidationConstants.Category.NameMaxLength)]
        [Comment("Name of the category")]
        public required string Name { get; set; }

        [StringLength(ValidationConstants.Category.DescriptionMaxLength)]
        [Comment("Optional description of the category")]
        public string? Description { get; set; }


        public virtual ICollection<Tool> Tools { get; set; } = new List<Tool>();
    }
}
