using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Budget
{
    public class MonthlyBudgetStatistics
    {
        public DateTime Month { get; set; }
        public decimal Profit { get; set; }
        /// <summary>
        /// A positive number.
        /// </summary>
        public decimal Cost { get; set; }
        public decimal Balance { get { return Profit - Cost; } }
    }

    public class LastNMonthsPreview
    {
        public decimal Profit { get { return LastNMonthsStatistics.Sum(i => i.Profit); } }
        /// <summary>
        /// A positive number.
        /// </summary>
        public decimal Cost { get { return LastNMonthsStatistics.Sum(i => i.Cost); } }
        public decimal Balance { get { return LastNMonthsStatistics.Sum(i => i.Balance); } }
        public List<MonthlyBudgetStatistics> LastNMonthsStatistics { get; set; }
        /// <summary>
        /// Keeps all budget items which are part of recurrent budget and which are expected for the next month.
        /// </summary>
        public List<BudgetItem> NextMonthExpectedBudgetItems { get; set; }
        public List<BudgetItem> ThisMonthBudgetItems { get; set; }
    }
}
