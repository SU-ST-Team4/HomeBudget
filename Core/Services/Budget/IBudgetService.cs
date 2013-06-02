using System;
using System.Collections.Generic;
using Core.Models.Budget;
using System.Linq.Expressions;

/// <summary>
/// A class which manipulates Budget related operations
/// </summary>
namespace Core.Services.Budget
{
    /// <summary>
    /// A class which manipulates Budget related operations
    /// </summary>
    public interface IBudgetService
    {
        int InsertBudgetItem(BudgetItem budgetItem);
        void UpdateBudgetItem(BudgetItem budgetItem);
        void DeleteBudgetItem(int id);
        List<BudgetItem> GetAllNonRecurrentBudgetItems(Expression<Func<BudgetItem, bool>> filter = null);
        List<BudgetCategory> GetAllBudgetCategories(Expression<Func<BudgetCategory, bool>> filter = null);
        int InsertBudgetCategory(BudgetCategory budgetCategory);
        void UpdateBudgetCategory(BudgetCategory budgetCategory);
        void DeleteBudgetCategory(int id);
        List<RecurrentBudget> GetAllRecurrentBudgets(Expression<Func<RecurrentBudget, bool>> filter = null);
        List<BudgetItem> GetAllRecurrentBudgetItems(Expression<Func<BudgetItem, bool>> filter = null);
        void InsertRecurrentBudget(RecurrentBudget recurrentBudget);
        void UpdateRecurrentBudget(RecurrentBudget recurrentBudget);
        void ApproveRecurrentBudgetItem(int budgetItemId, bool approve);
        LastNMonthsPreview GetLastNMonthBudgetPreviewByUserId(int userId, int numberOfMonths);
    }
}
