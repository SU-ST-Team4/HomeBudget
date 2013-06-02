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
        private readonly IGenericRepository<RecurrentBudget> _recurrentBudgetRepository;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="budgetCategoryRepository"></param>
        /// <param name="budgetItemRepository"></param>
        /// <param name="recurrentBudgetRepository"></param>
        public BudgetService(IGenericRepository<BudgetCategory> budgetCategoryRepository, IGenericRepository<BudgetItem> budgetItemRepository, IGenericRepository<RecurrentBudget> recurrentBudgetRepository)
        {
            if (budgetItemRepository == null)
            {
                throw new ArgumentNullException("budgetItemRepository");
            }

            if (budgetCategoryRepository == null)
            {
                throw new ArgumentNullException("budgetCategoryRepository");
            }

            if (recurrentBudgetRepository == null)
            {
                throw new ArgumentNullException("recurrentBudgetRepository");
            }

            _budgetItemRepository = budgetItemRepository;
            _budgetCategoryRepository = budgetCategoryRepository;
            _recurrentBudgetRepository = recurrentBudgetRepository;
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
        /// Get all non-recurrent budget items.
        /// </summary>
        /// <param name="budgetItem"></param>
        /// <returns></returns>
        public List<BudgetItem> GetAllNonRecurrentBudgetItems(Expression<Func<BudgetItem, bool>> filter = null)
        {
            return _budgetItemRepository.Get(filter, c => c.OrderByDescending(i => i.Date))
                                        .Where(i => i.RecurrentBudget == null)
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
        /// <summary>
        /// Get All recurrent budgets by filter.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<RecurrentBudget> GetAllRecurrentBudgets(Expression<Func<RecurrentBudget, bool>> filter = null)
        {
            return _recurrentBudgetRepository.Get(filter, c => c.OrderBy(i => i.StartDate))
                                             .ToList();
        }
        /// <summary>
        /// Get All recurrent budget items by filter.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<BudgetItem> GetAllRecurrentBudgetItems(Expression<Func<BudgetItem, bool>> filter = null)
        {
            return _budgetItemRepository.Get(filter, c => c.OrderBy(i => i.Date))
                                        .Where(bi => bi.RecurrentBudget != null)
                                        .ToList();
        }
        /// <summary>
        /// Inserts recurrent budget together with single budgetItems.
        /// </summary>
        /// <param name="recurrentBudget"></param>
        public void InsertRecurrentBudget(RecurrentBudget recurrentBudget)
        {
            if (recurrentBudget.StartDate.Day > 28)
            {
                recurrentBudget.StartDate = new DateTime(recurrentBudget.StartDate.Year, recurrentBudget.StartDate.Month, 28, 0, 0, 0);
            }

            _recurrentBudgetRepository.Insert(recurrentBudget);
            
            DateTime date = recurrentBudget.StartDate;

            for (int index = 0; index < recurrentBudget.Count; index++)
            {
                _budgetItemRepository.Insert(new BudgetItem()
                                            {
                                                Amount = recurrentBudget.Amount,
                                                BudgetCategory = recurrentBudget.BudgetCategory,
                                                Date = date,
                                                Description = recurrentBudget.Description,
                                                IsApproved = false,
                                                RecurrentBudget = recurrentBudget,
                                                UserProfile = recurrentBudget.UserProfile
                                            });
                date = date.AddMonths(1);
            }
            _budgetItemRepository.SaveChanges();
            _recurrentBudgetRepository.SaveChanges();
        }
        /// <summary>
        /// Updates recurrent budget with its budget items.
        /// </summary>
        /// <param name="recurrentBudget"></param>
        public void UpdateRecurrentBudget(RecurrentBudget recurrentBudget)
        {
            _recurrentBudgetRepository.Update(recurrentBudget);

            foreach (BudgetItem budgetItem in recurrentBudget.BudgetItems)
            {
                if (!budgetItem.IsApproved.Value)
                {
                    budgetItem.Amount = recurrentBudget.Amount;
                    budgetItem.Description = recurrentBudget.Description;
                    _budgetItemRepository.Update(budgetItem);
                }
            }

            _budgetItemRepository.SaveChanges();
            _recurrentBudgetRepository.SaveChanges();
        }
        /// <summary>
        /// Approve budget item which is part from recurrent budget.
        /// </summary>
        /// <param name="budgetItemId"></param>
        /// <param name="approve"></param>
        public void ApproveRecurrentBudgetItem(int budgetItemId,  bool approve)
        {
            BudgetItem item = _budgetItemRepository.GetByID(budgetItemId);
            if (item.RecurrentBudget != null)
            {
                item.IsApproved = approve;
                _budgetItemRepository.Update(item);
                _budgetItemRepository.SaveChanges();
            }
            else
            {
                throw new ApplicationException("you can't approve budget item which isn't part from recurrent budget.");
            }
        }
        /// <summary>
        /// Returns a preview with different budget statistics for the last several months filtered by userId.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="numberOfMonths">e.g. 3-> last 3 months</param>
        /// <returns></returns>
        public LastNMonthsPreview GetLastNMonthBudgetPreviewByUserId(int userId, int numberOfMonths)
        {
            LastNMonthsPreview result = new LastNMonthsPreview();
            result.LastNMonthsStatistics = new List<MonthlyBudgetStatistics>();

            DateTime currentMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1, 0, 0, 0);
            DateTime iterationMonth = currentMonth.AddMonths(- numberOfMonths + 1);

            for (int monthIndex = 1; monthIndex <= numberOfMonths; monthIndex++)
            {
                MonthlyBudgetStatistics currentMonthlyBudgetStatistics = new MonthlyBudgetStatistics();
                currentMonthlyBudgetStatistics.Month = iterationMonth;

                List<BudgetItem> currentApprovedOrNonRecurrentMonthItems = _budgetItemRepository.Get(i => i.UserProfile.UserId == userId && 
                                                                                                          i.Date >= iterationMonth &&
                                                                                                          i.Date < iterationMonth.AddMonths(1) &&
                                                                                                          (!i.IsApproved.HasValue || i.IsApproved.Value))
                                                                                                .ToList();

                //the cost must be positive
                currentMonthlyBudgetStatistics.Cost = currentApprovedOrNonRecurrentMonthItems.Where(i => i.Amount < 0)
                                                                                             .Sum(i => - i.Amount);
                currentMonthlyBudgetStatistics.Profit = currentApprovedOrNonRecurrentMonthItems.Where(i => i.Amount >= 0)
                                                                                               .Sum(i => i.Amount);

                result.LastNMonthsStatistics.Add(currentMonthlyBudgetStatistics);

                iterationMonth = iterationMonth.AddMonths(monthIndex);
            }


            result.ThisMonthBudgetItems = _budgetItemRepository.Get(i => i.UserProfile.UserId == userId &&
                                                                         i.Date >= currentMonth &&
                                                                         i.Date < currentMonth.AddMonths(1) &&
                                                                         (!i.IsApproved.HasValue || i.IsApproved.Value))
                                                               .ToList();

            result.NextMonthExpectedBudgetItems = _budgetItemRepository.Get(i => i.UserProfile.UserId == userId &&
                                                                                 i.Date >= currentMonth.AddMonths(1) &&
                                                                                 i.Date < currentMonth.AddMonths(2))
                                                                       .ToList();

            return result;
        }

        #endregion Methods
    }
}