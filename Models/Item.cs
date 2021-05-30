using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Presenteie.Models.Enum;

namespace Presenteie.Models
{
    public class Item
    {
        [Key] 
        public long Id { get; set; }
        
        [ForeignKey("List")]
        public long IdList { get; set; }
       
        [Required]
        public string Name { get; set; }
       
        [Required]
        public string Description { get; set; }
       
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public Decimal Value { get; set; }
        
        [Required]
        [Column(TypeName = "nvarchar(15)")]
        public State State { get; set; }
       
        [Required]
        public String StoreName { get; set; }
        
        [Required]
        public string Link { get; set; }
    }
}