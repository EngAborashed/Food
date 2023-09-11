using MessagePack;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Food.Models
{
    [PrimaryKey(nameof(CustomerId), nameof(ItemsId))]
    public class Customer_Item
    {
        [System.ComponentModel.DataAnnotations.Key] 
        public int CustomerId { get; set; }
        
        public int ItemsId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer customer { get; set; }
        [ForeignKey("ItemsId")]
        public Items Items { get; set; }
    }
}
