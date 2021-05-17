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
        public Decimal Value { get; set; }
        
        [Required]
        public State state { get; set; }
       
        [Required]
        public string Link { get; set; }
    }
}