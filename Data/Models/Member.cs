using System.ComponentModel.DataAnnotations;

namespace CommuniLend.Data.Models
{
    public class Member
    {
        [Key]
        public int Id { get; set; }


        [Required]
        public required string FullName { get; set; }



        [Required]
        public required string Email { get; set; }


        [Required]
        public required string PhoneNumber { get; set; }



        public string? Address { get; set; }



        [Required]
        public DateOnly JoinDate { get; set; }


        [Required]
        public required bool IsActive { get; set; }


    }
}
