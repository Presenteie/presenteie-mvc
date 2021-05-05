using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Presenteie.Models
{
    public class User
    {
        [Key]
        public long Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }
        
        public List<List> List { get; set; } 
    }
}