using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Presenteie.Models
{
    public class Security
    {
        [Key]
        public long Id { get; set; }
        
        [ForeignKey("User")]
        public long IdUser { get; set; }
        
        [Required]
        public string Hash { get; set; }
        
        [Required]
        public DateTime ExpiresAt { get; set; }
        
        [Required]
        public DateTime CreatedAt { get; set; }
    }
}