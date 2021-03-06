using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Presenteie.Models
{
    public class User
    {
        [Key]
        public long Id { get; set; }
        
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "O email deve ser válido")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "A senha é obrigatória")]
        public string Password { get; set; }
        
        [NotMapped]
        [Compare("Password", ErrorMessage = "Confirmar senha não confere.")]
        public string ConfirmPassword { get; set; }
    }
    
    public class UserCredentials
    {
        [Required(ErrorMessage = "O email é obrigatório")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "A senha é obrigatória")]
        public string Password { get; set; }
        
        [DefaultValue(false)]
        public bool RememberMe { get; set; } 
        
        public string ReturnUrl { get; set; } 
    }
}