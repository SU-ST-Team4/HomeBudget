using System;
using System.Collections.Generic;
using Core.Models.Budget;

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
        List<BudgetItem> GetAllBudgetItems(BudgetItem budgetItem);
        List<BudgetCategory> GetAllBudgetCategories();
        int InsertBudgetCategory(BudgetCategory budgetCategory);
        void UpdateBudgetCategory(BudgetCategory budgetCategory);
        void DeleteBudgetCategory(int id);
    }
}
