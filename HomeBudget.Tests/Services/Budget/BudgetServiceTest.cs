using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ApplicationServices.Services.Budget;
using Core.Services.Budget;
using Core.Models.Budget;
using Core.Data;
using System.Linq.Expressions;

namespace HomeBudget.Tests.Budget.Services
{
    [TestClass]
    public class BudgetServiceTest
    {
        [TestMethod]
        public void BudgetService_Constructor_Test_With_Correct_Arguments()
        {
            var categoryRepoMock = new Mock<IGenericRepository<BudgetCategory>>();
            var budgetItemRepoMock = new Mock<IGenericRepository<BudgetItem>>();
            var instance = new BudgetService(categoryRepoMock.Object, budgetItemRepoMock.Object);
            Assert.IsNotNull(instance);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BudgetService_Constructor_Test_With_Incorrect_Arguments()
        {
            var categoryRepoMock = new Mock<IGenericRepository<BudgetCategory>>();
            var instance = new BudgetService(categoryRepoMock.Object, null);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BudgetService_Constructor_Test_With_Incorrect_Arguments2()
        {
            var budgetItemRepoMock = new Mock<IGenericRepository<BudgetItem>>();
            var instance = new BudgetService(null, budgetItemRepoMock.Object);
        }
        [TestMethod]
        public void BudgetService_InsertBudgetCategory_With_Correct_Arguments()
        {
            BudgetCategory category = new BudgetCategory() { Id = 5, Name = "Pesho"};

            var categoryRepoMock = new Mock<IGenericRepository<BudgetCategory>>();
            categoryRepoMock.Setup(c => c.Insert(category));
            categoryRepoMock.Setup(c => c.SaveChanges());

            var budgetItemRepoMock = new Mock<IGenericRepository<BudgetItem>>();

            var instance = new BudgetService(categoryRepoMock.Object, budgetItemRepoMock.Object);

            instance.InsertBudgetCategory(category);
            categoryRepoMock.VerifyAll();
        }
        [TestMethod]
        public void BudgetService_UpdateBudgetCategory_With_Correct_Arguments()
        {
            BudgetCategory category = new BudgetCategory() { Id = 5, Name = "Pesho" };

            var categoryRepoMock = new Mock<IGenericRepository<BudgetCategory>>();
            categoryRepoMock.Setup(c => c.Update(category));
            categoryRepoMock.Setup(c => c.SaveChanges());

            var budgetItemRepoMock = new Mock<IGenericRepository<BudgetItem>>();

            var instance = new BudgetService(categoryRepoMock.Object, budgetItemRepoMock.Object);

            instance.UpdateBudgetCategory(category);
            categoryRepoMock.VerifyAll();
        }

        [TestMethod]
        public void BudgetService_GetAllBudgetCategories_With_Correct_Arguments()
        {
            var categoryRepoMock = new Mock<IGenericRepository<BudgetCategory>>();
            categoryRepoMock.Setup(c => c.Get(It.IsAny<Expression<Func<BudgetCategory, bool>>>(),
                                              It.IsAny<Func<IQueryable<BudgetCategory>, IOrderedQueryable<BudgetCategory>>>(),
                                              It.IsAny<string>()))
                            .Returns(new List<BudgetCategory>() { new BudgetCategory() { Id = 5, Name = "Pesho" } });

            var budgetItemRepoMock = new Mock<IGenericRepository<BudgetItem>>();

            var instance = new BudgetService(categoryRepoMock.Object, budgetItemRepoMock.Object);

            var result = instance.GetAllBudgetCategories();
            Assert.IsTrue(result.Count == 1);
            categoryRepoMock.VerifyAll();
        }

        [TestMethod]
        public void BudgetService_DeleteBudgetCategory_By_Id()
        {
            BudgetCategory category = new BudgetCategory() { Id = 5, Name = "Pesho" };

            var categoryRepoMock = new Mock<IGenericRepository<BudgetCategory>>();
            categoryRepoMock.Setup(c => c.Delete(category.Id));
            categoryRepoMock.Setup(c => c.SaveChanges());

            var budgetItemRepoMock = new Mock<IGenericRepository<BudgetItem>>();

            var instance = new BudgetService(categoryRepoMock.Object, budgetItemRepoMock.Object);

            instance.DeleteBudgetCategory(category.Id);
            categoryRepoMock.VerifyAll();
        }
    }
}
