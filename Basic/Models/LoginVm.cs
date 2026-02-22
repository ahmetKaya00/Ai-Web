using System.ComponentModel.DataAnnotations;

namespace Basic.Models
{
    
    public class Loginvm
    {
        [Required, EmailAddress]
        public string Email {get;set;} = "";
        [Required, DataType(DataType.Password)]
        public string Password {get;set;} = "";
        public bool RememberMe {get;set;}
    }
}