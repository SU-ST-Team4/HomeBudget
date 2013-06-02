using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Core.Models.Authentication;
using Core.Models.Budget;

namespace Infrastructure.Data
{
    public class HomeBudgetContext : DbContext
    {
        public HomeBudgetContext()
            : base("HomeBudget")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<BudgetItem> BudgetItems { get; set; }
        public DbSet<BudgetCategory> BudgetCategories { get; set; }
        public DbSet<RecurrentBudget> RecurrentBudgets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
