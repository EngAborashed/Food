using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Food.Models
{
    public class User_login
    {
        [DefaultValue(0)]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        


    }
}
