using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Presenteie.Models
{
    public class Item
    {
       [Key]
       [ForeignKey("List")]
       public long IdList { get; set; }
       
       [Required]
       public string Name { get; set; }
       
       [Required]
       public string Description { get; set; }
       
       [Required]
       public Decimal Value { get; set; }
       
       [Required]
       public string Link { get; set; }
    }
}