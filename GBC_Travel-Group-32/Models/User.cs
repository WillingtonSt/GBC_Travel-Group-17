using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace GBC_Travel_Group_32.Models {
    public class User : IdentityUser {


        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string? FrequentFlyerNumber { get; set; }

        public string? HotelLoyaltyID { get; set; }

        public byte[]? ProfilePicture { get; set; }

       

      
    }






}
