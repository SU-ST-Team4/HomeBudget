using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Data;
using Core.Models.Budget;
using Core.Services.Budget;

namespace ApplicationServices.Services.Budget
{
    /// <summary>
    /// A class which manipulates Budget related operations
    /// </summary>
    public class BudgetService : IBudgetService
    {
        #region Fields

        private readonly IGenericRepository<BudgetCategory> _budgetCategoryRepository;
        private readonly IGenericRepository<BudgetItem> _budgetItemRepository;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="budgetCategoryRepository"></param>
        public BudgetService(IGenericRepository<BudgetCategory> budgetCategoryRepository, IGenericRepository<BudgetItem> budgetItemRepository)
        {
            if (budgetItemRepository == null)
            {
                throw new ArgumentNullException("budgetItemRepository");
            }

            if (budgetCategoryRepository == null)
            {
                throw new ArgumentNullException("budgetCategoryRepository");
            }

            _budgetItemRepository = budgetItemRepository;
            _budgetCategoryRepository = budgetCategoryRepository;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Add budget item into the system.
        /// </summary>
        /// <param name="budgetItem"></param>
        /// <returns></returns>
        public int AddBudgetItem(BudgetItem budgetItem)
        {
            int result = _budgetItemRepository.Insert(budgetItem);
            _budgetCategoryRepository.SaveChanges();

            return result;
        }
        /// <summary>
        /// Gets all budget categories.
        /// </summary>
        /// <returns></returns>
        public List<BudgetCategory> GetAllBudgetCategories()
        {
            return _budgetCategoryRepository.Get(null, c => c.OrderBy(i => i.Name))
                                            .ToList();
        }

        #endregion Methods
    }
}