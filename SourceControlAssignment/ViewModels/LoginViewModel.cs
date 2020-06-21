using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SourceControlAssignment.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [DisplayName("Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }
    }
}