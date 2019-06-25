namespace Eventures.Web.ViewModels.Account
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterBindingModel
    {
        [Required]
        [MinLength(3, ErrorMessage = "Username must be atleast 3 characters long!")]
        [RegularExpression(@"^[A-Za-z0-9\-_\*~]+$", ErrorMessage = "Username can only contain alphanumerical characters, dashes, underscores, asterisks and tildas!")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Password should be atleast 5 characters long!")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"\d{10}", ErrorMessage = "The Universal Citizen Number must consist of exactly 10 numbers!")]
        [Display(Name = "Universal Citizen Number")]
        public string UniversalCitizenNumber { get; set; }
    }
}