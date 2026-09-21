using CommuniLend.DataConstraints;
using System.ComponentModel.DataAnnotations;

namespace CommuniLend.Data.Models
{
    public class Category
    {

        [Key]
        public int Id { get; set; }


        [Required]
        [StringLength(ValidationConstants.Category.NameMaxLength)]
        public required string Name { get; set; }

        [StringLength(ValidationConstants.Category.DescriptionMaxLength)]
        public string? Description { get; set; }


        public virtual ICollection<Tool> Tools { get; set; } = new List<Tool>();
    }
}
