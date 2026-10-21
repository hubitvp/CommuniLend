using CommuniLend.Data.Models.Constraints;
using CommuniLend.Data.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommuniLend.Data.Models
{
    [Comment("Tool that a member shares with the community")]
    public class Tool
    {
        [Key]
        [Comment("Primary key of the tool")]
        public int Id { get; set; }


        [Required]
        [StringLength(ValidationConstants.Tool.NameMaxLength)]
        [Comment("Name of the tool")]
        public required string Name { get; set; }


        [Required]
        [StringLength(ValidationConstants.Tool.DescriptionMaxLength)]
        [Comment("Description of the tool")]
        public required string Description { get; set; }


        [Required]
        [Comment("Physical condition of the tool (ToolCondition enum value)")]
        public required ToolCondition Condition { get; set; }


        [Required]
        [Column(TypeName = ColumnTypes.Tool.Decimal)]
        [Comment("Replacement value of the tool in EUR")]
        public required decimal ReplacementValue { get; set; }


        [StringLength(ValidationConstants.Tool.ImageUrlMaxLength)]
        [Comment("Optional relative URL of the tool image")]
        public required string? ImageUrl { get; set; }


        [Required]
        [Comment("Date the tool was added to the library")]
        public required DateOnly DateAdded { get; set; }


        [Required]
        [Comment("Foreign key to the category of the tool")]
        public required int CategoryId { get; set; }


        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; } = null!;


        [Required]
        [StringLength(ValidationConstants.User.ApplicationUserIdMaxLength)]
        [Comment("Foreign key to the member who owns the tool")]
        public required string OwnerId { get; set; }


        [ForeignKey(nameof(OwnerId))]
        public virtual ApplicationUser Owner { get; set; } = null!;


        public virtual OpenLoan? OpenLoan { get; set; }



        public virtual ICollection<ClosedLoan> ClosedLoans { get; set; } = new List<ClosedLoan>();
    }
}
