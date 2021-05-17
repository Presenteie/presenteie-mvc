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
        // Id do usuário que criou essa lista
        [Key]
        [ForeignKey("User")]
        public long IdUser { get; set; }

        // Tema da lista
        [Required] 
        public Theme ThemeList { get; set; }

        // TODO Existe alguma classe melhor para representar data? 
        public DateTime Date { get; set; }
    }
    
}