using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;  

namespace Food.Models
{
    public class Customer_Feedback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FeedId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required]
        public int Cleens { get; set; }
        [Required]
        public int Service { get; set; }
        [Required]
        public int FoodQuality { get; set; }
        [Required]
        public string Suggetions { get; set; }
        public int? CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public Customer? Customer { get; set; }


    }  
} 
