using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Food.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string FoodName{ get; set; }
        public int? custmoer_Id { get; set; }
        [ForeignKey("custmoer_Id")]
        public Customer? custmoer { get; set; }
        
    }
}
