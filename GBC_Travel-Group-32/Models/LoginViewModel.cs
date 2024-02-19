using System.ComponentModel.DataAnnotations;

namespace GBC_Travel_Group_32.Models {
    public class LoginViewModel {


        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }


    }
}
