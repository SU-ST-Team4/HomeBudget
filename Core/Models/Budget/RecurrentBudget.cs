using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Models.Authentication;

namespace Core.Models.Budget
{
    [Table("RecurrentBudget")]
    public class RecurrentBudget : IEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime? StartDate { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        public int Count { get; set; }
        public decimal Amount { get; set; }
        [StringLength(300)]
        public string Description { get; set; }
        public int BudgetCategory_Id { get; set; }
        [ForeignKey("BudgetCategory_Id")]
        public virtual BudgetCategory BudgetCategory { get; set; }
        public ICollection<BudgetItem> BudgetItems { get; set; }
    }
}
