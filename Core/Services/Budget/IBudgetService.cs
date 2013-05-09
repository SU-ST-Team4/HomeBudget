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
        int AddBudgetItem(BudgetItem budgetItem);
        List<BudgetCategory> GetAllBudgetCategories();
    }
}
