using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Food.Models
{
    public class Payment
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentId { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string PaymentType { get; set; }
        public long? CardNum { get; set; }
        public int? custmoer_Id { get; set; }
        [ForeignKey("custmoer_Id")]
        public Customer? custmoer { get; set; }

    }
}
