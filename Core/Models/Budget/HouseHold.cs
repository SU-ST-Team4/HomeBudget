using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Models;
using Core.Models.Authentication;
using HomeBudget.Helpers.Validators;

namespace Core.Models.Budget
{
    [Table("HouseHolds")]
    public class HouseHold : IEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public virtual UserProfile First { get; set; }
        public virtual UserProfile Second { get; set; }
        public bool? IsApproved { get; set; }
        public DateTime? ApproveDate { get; set; }
        [StringLength(300)]
        public string RequestMessage { get; set; }
    }
}
