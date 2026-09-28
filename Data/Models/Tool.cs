using CommuniLend.Data.Models.Enums;
using CommuniLend.DataConstraints;
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


        public string? ImageUrl { get; set; }


        [Required]
        public required bool IsAvailable { get; set; }


        [Required]
        public required DateOnly DateAdded { get; set; }


        [Required]
        public required int CategoryId { get; set; }


        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; } = null!;


        [Required]
        public required int OwnerId { get; set; }


        [ForeignKey(nameof(OwnerId))]
        public virtual Member Owner { get; set; } = null!;


        public virtual OpenLoan? OpenLoan { get; set; }


        public virtual ICollection<ClosedLoan> ClosedLoans { get; set; } = new List<ClosedLoan>();
    }
}
