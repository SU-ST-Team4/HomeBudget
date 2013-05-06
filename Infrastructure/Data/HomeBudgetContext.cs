using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Core.Models.Authentication;

namespace Infrastructure.Data
{
    public class HomeBudgetContext : DbContext
    {
        public HomeBudgetContext()
            : base("HomeBudget")
        {
        }

        public DbSet<UserExample> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
    }
}
