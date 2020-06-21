using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace SourceControlAssignment.ViewModels
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "Please enter first name")]
        [DisplayName("First Name")]
        [MaxLength(25)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter last name")]
        [DisplayName("First Name")]
        [MaxLength(25)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Enter your email id.")]
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        [Remote("IsEmailExist", "Account", HttpMethod = "POST", ErrorMessage = "EmailId already exists.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter your phone number.")]
        [DisplayName("Phone")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please select your birthdate")]
        [DisplayName("Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Birthdate { get; set; }

        [Required(ErrorMessage = "Please enter your height in cms.")]
        [DisplayName("Height (in cms)")]
        [Range(15,250, ErrorMessage = "Height Must be between 15 to 250 cms.")]
        public byte Height { get; set; }

        [Required(ErrorMessage = "Please enter your weight in kgs.")]
        [DisplayName("Weight (in kgs)")]
        [Range(1, 135, ErrorMessage = "Height Must be between 1 to 135 kgs.")]
        public byte Weight { get; set; }

        [Attributes.FileExtensions("jpg,gif,png,jpeg", ErrorMessage = "Upload only image file of type jpg, gif, png or jpeg.")]
        public HttpPostedFileBase Photo { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(18, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must contain Caps, number and special character.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Key]
        public Guid rowguid { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ImageSrc { get; set; }
    }
}