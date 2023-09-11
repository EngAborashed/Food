using Food.Data;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Food.Models
{
    public class Customer
    {
        private readonly ApplicationDbcontext _dbcontext;
      
        public Customer(ApplicationDbcontext context)
        {
            _dbcontext = context;
          
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "FirstName is Required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName is Required")]
        public string LastName { get; set; }
        
        [Required]
        [DataType(DataType.EmailAddress)]
        [Remote(action: "IsEmailExist",controller:"Acount",ErrorMessage ="Email is already used")]
        public string Email { get; set; }
       

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "not match")]
        public string ConfirmPassword { get; set; } 
        public string? Status { get; set; }
        public string? FullName { get; set; }
        public string? uesrpicture { get; set; }
        [NotMapped]
        //public string userCheck
        //{
        //    get
        //    {
        //        string Email = _dbcontext.Customers.Email;

        //    }
        //}
        public ICollection<Customer_Feedback>? Feedbacks { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<Customer_Item>? customer_Items { get; set; }
        public List<Payment>? customer_Payment { get; set; }    
        
    }

}
   

