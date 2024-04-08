using System.ComponentModel.DataAnnotations;

namespace GBC_Travel_Group_32.Models {
    public class ListingReview {


        [Key]
        public int ListingReviewId { get; set; }

        [Required]
        [Display(Name = "Review")]
        [StringLength(500, ErrorMessage = "Review cannot exceed 500 characters")]
        public string? Content { get; set; }

        [Display(Name = "Posted Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DatePosted { get; set; }

        public int ListingId {  get; set; }

        public string UserId { get; set; }
        
        public Listing? Listing { get; set; }


    }
}
