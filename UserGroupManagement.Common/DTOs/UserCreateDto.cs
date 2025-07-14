using System.ComponentModel.DataAnnotations;

namespace UserGroupManagement.Common.DTOs
{
    public class UserCreateDto
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Range(0, 60)]
        public int Age { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
    }
}
