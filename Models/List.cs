using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Presenteie.Models.Enum;

/*
     The list class
 */

namespace Presenteie.Models
{
    public class List
    {
        [Key] 
        public long Id { get; set; }

        // Id do usuário que criou essa lista
        [ForeignKey("User")]
        public long IdUser { get; set; }

        // Tema da lista
        [Required] 
        public Theme ThemeList { get; set; }
        
        [Required]
        public DateTime CreatedDate { get; set; }
        
        [Required]
        public DateTime EventDate { get; set; }
    }
    
}