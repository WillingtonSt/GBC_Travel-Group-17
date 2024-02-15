using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GBC_Travel_Group_32.Models {
    public class User : IdentityUser {

 

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }




    }
}
