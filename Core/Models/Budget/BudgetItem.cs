using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Models;
using Core.Models.Authentication;

namespace Core.Models.Budget
{
    [Table("BudgetItems")]
    public class BudgetItem : IEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? Date { get; set; }
        public int BudgetCategory_Id { get; set; }
        [ForeignKey("BudgetCategory_Id")]
        public BudgetCategory BudgetCategory { get; set; }
        public decimal Amount { get; set; }
        [StringLength(300)]
        public string Description { get; set; }
        public virtual RecurrentBudget RecurrentBudget { get; set; }
        public bool? IsApproved { get; set; }
    }
}
