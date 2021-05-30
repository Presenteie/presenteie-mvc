using System;
using System.ComponentModel.DataAnnotations;

namespace Presenteie.Models
{
    public class Security
    {
        [Key]
        public long Id { get; init; }
        
        [Required]
        public long UserId { get; init; }
        
        [Required]
        public string Hash { get; init; }
        
        [Required]
        public DateTime ExpiresAt { get; init; }
        
        [Required]
        public DateTime CreatedAt { get; init; }
    }
}