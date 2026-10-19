using CommuniLend.Data.Models.Constraints;
using CommuniLend.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommuniLend.Data.Models
{
    public class Tool
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [StringLength(ValidationConstants.Tool.NameMaxLength)]
        public required string Name { get; set; }


        [Required]
        [StringLength(ValidationConstants.Tool.DescriptionMaxLength)]
        public required string Description { get; set; }


        [Required]
        public required ToolCondition Condition { get; set; }


        [Required]
        [Column(TypeName = ColumnTypes.Tool.Decimal)]
        public required decimal ReplacementValue { get; set; }


        [StringLength(ValidationConstants.Tool.ImageUrlMaxLength)]
        public required string? ImageUrl { get; set; }


        [Required]
        public required DateOnly DateAdded { get; set; }


        [Required]
        public required int CategoryId { get; set; }


        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; } = null!;


        [Required]
        [StringLength(ValidationConstants.User.ApplicationUserIdMaxLength)]
        public required string OwnerId { get; set; }


        [ForeignKey(nameof(OwnerId))]
        public virtual ApplicationUser Owner { get; set; } = null!;


        public virtual OpenLoan? OpenLoan { get; set; }


        public virtual ICollection<ClosedLoan> ClosedLoans { get; set; } = new List<ClosedLoan>();
    }
}
