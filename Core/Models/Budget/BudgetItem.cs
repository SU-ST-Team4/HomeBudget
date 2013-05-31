using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Models;

namespace Core.Models.Budget
{
    [Table("BudgetItems")]
    public class BudgetItem : IEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public BudgetCategory BudgetCategory { get; set; }
        public decimal Amount { get; set; }
        [StringLength(300)]
        public string Description { get; set; }

    }
}
