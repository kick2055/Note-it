using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace projekt.Models
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "decimal(6,2)")]
        [Range(0,9999)]
        public decimal Price { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;
        [Required]
        public string Name { get; set; }
        public CategoryMoney ThisCategoryMoney { get; set; } = null;

    }
}
