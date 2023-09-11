using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Food.Models
{
    public class Items
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemId { get; set; }
        [Required]
        public string ItemName { get; set; }
       
        [Required]
        public string Description { get; set; }
        [Display(Name = "Image")]
        [DefaultValue("default.png")]
        public string Item_Pic { get; set; }
        public decimal ItemPrice { get; set; }
        public ICollection<Customer_Item>? customer_Items { get; set; }
    }
}