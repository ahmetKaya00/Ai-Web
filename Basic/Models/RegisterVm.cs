using System.ComponentModel.DataAnnotations;

namespace Basic.Models
{
    
    public class RegisterVm
    {
        [Required]
        public string FullName {get;set;} = "";
        [Required, EmailAddress]
        public string Email {get;set;} = "";
        [Required, DataType(DataType.Password)]
        public string Password {get;set;} = "";
        
        [Required,DataType(DataType.Password),Compare(nameof(Password))]
        public string ConfirmPassword {get;set;} = "";
    }
}