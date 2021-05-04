using System.ComponentModel.DataAnnotations;

namespace Presenteie.Models
{
    public class User
    {
        [Key]
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public string Email { get; set; }
        
        public string Password { get; set; }
    }
}