using Core.Models.Budget;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomeBudget.Tests.Models.Budget
{   
    [TestClass]
    public class BudgetCategoryTest
    {
        [TestMethod]
        public void BudgetCategory_Constructor_Test()
        {
            BudgetCategory category = new BudgetCategory();
            Assert.IsNotNull(category);
        }

        [TestMethod]
        public void BudgetCategory_Id_Test()
        {
            BudgetCategory category = new BudgetCategory();
            int expected = 5;
            int actual;
            category.Id = expected;
            actual = category.Id;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BudgetCategory_Name_Test()
        {
            BudgetCategory category = new BudgetCategory();
            string expected = "Boicho";
            string actual;
            category.Name = expected;
            actual = category.Name;
            Assert.AreEqual(expected, actual);
        }     
    }
}
