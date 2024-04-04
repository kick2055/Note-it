using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projekt.Models
{
    public class CategoryMoney
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string Name { get; set; }


    }
}
