using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Data;
using Core.Models.Budget;
using Core.Services.Budget;
using System.Linq.Expressions;

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
        /// Gets all budget categories.
        /// </summary>
        /// <returns></returns>
        public List<BudgetCategory> GetAllBudgetCategories(Expression<Func<BudgetCategory, bool>> filter = null)
        {
            return _budgetCategoryRepository.Get(filter, c => c.OrderBy(i => i.Name))
                                            .ToList();
        }
        /// <summary>
        /// Insert budget category
        /// </summary>
        /// <returns></returns>
        public int InsertBudgetCategory(BudgetCategory budgetCategory)
        {
            int result = _budgetCategoryRepository.Insert(budgetCategory);
            _budgetCategoryRepository.SaveChanges();

            return result;
        }
        /// <summary>
        /// Updates budget category
        /// </summary>
        /// <param name="budgetCategory"></param>
        public void UpdateBudgetCategory(BudgetCategory budgetCategory)
        {
            _budgetCategoryRepository.Update(budgetCategory);
            _budgetCategoryRepository.SaveChanges();
        }
        /// <summary>
        /// Deletes a budgetCategory
        /// </summary>
        /// <param name="id"></param>
        public void DeleteBudgetCategory(int id)
        {
            _budgetCategoryRepository.Delete(id);
            _budgetCategoryRepository.SaveChanges();
        }
        /// <summary>
        /// Get all budget items.
        /// </summary>
        /// <param name="budgetItem"></param>
        /// <returns></returns>
        public List<BudgetItem> GetAllBudgetItems(Expression<Func<BudgetItem, bool>> filter = null)
        {
            return _budgetItemRepository.Get(filter, c => c.OrderByDescending(i => i.Date))
                                        .ToList();
        }
        /// <summary>
        /// Add budget item into the system.
        /// </summary>
        /// <param name="budgetItem"></param>
        /// <returns></returns>
        public int InsertBudgetItem(BudgetItem budgetItem)
        {
            int result = _budgetItemRepository.Insert(budgetItem);
            _budgetItemRepository.SaveChanges();

            return result;
        }
        /// <summary>
        /// Updates a budgetItem
        /// </summary>
        /// <param name="budgetItem"></param>
        public void UpdateBudgetItem(BudgetItem budgetItem)
        {
            _budgetItemRepository.Update(budgetItem);
            _budgetItemRepository.SaveChanges();
        }
        /// <summary>
        /// Deletes a budgetItem
        /// </summary>
        /// <param name="budgetItem"></param>
        public void DeleteBudgetItem(int id)
        {
            _budgetItemRepository.Delete(id);
            _budgetItemRepository.SaveChanges();
        }

        #endregion Methods
    }
}