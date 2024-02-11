using System.ComponentModel.DataAnnotations;

namespace GBC_Travel_Group_32.Models {
    public class Listing {

        [Key]
        public int ListingId { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        public float Price { get; set; }

        [Required]
        public int UserId {  get; set; }
        


    }
}
