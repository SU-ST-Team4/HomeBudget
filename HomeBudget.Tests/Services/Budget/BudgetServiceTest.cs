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
using Core.Models.Authentication;
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
            var recurrentBudgetRepository = new Mock<IGenericRepository<RecurrentBudget>>();
            var instance = new BudgetService(categoryRepoMock.Object, budgetItemRepoMock.Object, recurrentBudgetRepository.Object);
            Assert.IsNotNull(instance);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BudgetService_Constructor_Test_With_Incorrect_Second_Argument()
        {
            var categoryRepoMock = new Mock<IGenericRepository<BudgetCategory>>();
            var recurrentBudgetRepository = new Mock<IGenericRepository<RecurrentBudget>>();
            var instance = new BudgetService(categoryRepoMock.Object, null, recurrentBudgetRepository.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BudgetService_Constructor_Test_With_Incorrect_First_Argument()
        {
            var budgetItemRepoMock = new Mock<IGenericRepository<BudgetItem>>();
            var recurrentBudgetRepository = new Mock<IGenericRepository<RecurrentBudget>>();
            var instance = new BudgetService(null, budgetItemRepoMock.Object, recurrentBudgetRepository.Object);
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
            var recurrentBudgetRepository = new Mock<IGenericRepository<RecurrentBudget>>();

            var instance = new BudgetService(categoryRepoMock.Object, budgetItemRepoMock.Object, recurrentBudgetRepository.Object);

            var result = instance.GetAllBudgetCategories();
            Assert.IsTrue(result.Count == 1);
            categoryRepoMock.VerifyAll();
        }

        [TestMethod]
        public void BudgetService_InsertBudgetCategory_With_Correct_Arguments()
        {
            BudgetCategory category = new BudgetCategory() { Id = 5, Name = "Pesho"};

            var categoryRepoMock = new Mock<IGenericRepository<BudgetCategory>>();
            categoryRepoMock.Setup(c => c.Insert(category));
            categoryRepoMock.Setup(c => c.SaveChanges());

            var budgetItemRepoMock = new Mock<IGenericRepository<BudgetItem>>();
            var recurrentBudgetRepository = new Mock<IGenericRepository<RecurrentBudget>>();

            var instance = new BudgetService(categoryRepoMock.Object, budgetItemRepoMock.Object, recurrentBudgetRepository.Object);

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
            var recurrentBudgetRepository = new Mock<IGenericRepository<RecurrentBudget>>();

            var instance = new BudgetService(categoryRepoMock.Object, budgetItemRepoMock.Object, recurrentBudgetRepository.Object);

            instance.UpdateBudgetCategory(category);
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
            var recurrentBudgetRepository = new Mock<IGenericRepository<RecurrentBudget>>();

            var instance = new BudgetService(categoryRepoMock.Object, budgetItemRepoMock.Object, recurrentBudgetRepository.Object);

            instance.DeleteBudgetCategory(category.Id);
            categoryRepoMock.VerifyAll();
        }

        [TestMethod]
        public void BudgetService_GetAllBudgetItems_With_Correct_Arguments()
        {
            var itemRepoMock = new Mock<IGenericRepository<BudgetItem>>();
            itemRepoMock.Setup(c => c.Get(It.IsAny<Expression<Func<BudgetItem, bool>>>(),
                                          It.IsAny<Func<IQueryable<BudgetItem>, IOrderedQueryable<BudgetItem>>>(),
                                          It.IsAny<string>()))
                                     .Returns(new List<BudgetItem>() { new BudgetItem()
                                                                        {
                                                                            Id = 6, 
                                                                            Amount=21,
                                                                            BudgetCategory = new BudgetCategory () {Id = 3, Name = "Dragan" },
                                                                            Date = System.DateTime.MaxValue,
                                                                            Description = "Some Description", 
                                                                            UserProfile = new UserProfile() { UserId = 15}
                                                                         }
                                                                      });

            var budgetCategoryRepoMock = new Mock<IGenericRepository<BudgetCategory>>();
            var recurrentBudgetRepository = new Mock<IGenericRepository<RecurrentBudget>>();

            var instance = new BudgetService(budgetCategoryRepoMock.Object, itemRepoMock.Object, recurrentBudgetRepository.Object);

            var result = instance.GetAllNonRecurrentBudgetItems();
            Assert.IsTrue(result.Count == 1);
            itemRepoMock.VerifyAll();
        }

        [TestMethod]
        public void BudgetService_InsertBudgetItem_With_Correct_Arguments()
        {
            BudgetItem item = new BudgetItem() {    
                                                    Id = 6, 
                                                    Amount=21,
                                                    BudgetCategory = new BudgetCategory () {Id = 3, Name = "Dragan" },
                                                    Date = System.DateTime.MaxValue,
                                                    Description = "Some Description",
                                                    UserProfile = new UserProfile() { UserId = 15 }
                                                };

            var itemRepoMock = new Mock<IGenericRepository<BudgetItem>>();
            itemRepoMock.Setup(c => c.Insert(item));
            itemRepoMock.Setup(c => c.SaveChanges());

            var budgetCategoryRepoMock = new Mock<IGenericRepository<BudgetCategory>>();
            var recurrentBudgetRepository = new Mock<IGenericRepository<RecurrentBudget>>();

            var instance = new BudgetService(budgetCategoryRepoMock.Object, itemRepoMock.Object, recurrentBudgetRepository.Object);

            instance.InsertBudgetItem(item);
            itemRepoMock.VerifyAll();
        }

        [TestMethod]
        public void BudgetService_UpdateBudgetItem_With_Correct_Arguments()
        {
            BudgetItem item = new BudgetItem() {
                                                    Id = 6,
                                                    Amount = 21,
                                                    BudgetCategory = new BudgetCategory() { Id = 3, Name = "Dragan" },
                                                    Date = System.DateTime.MaxValue,
                                                    Description = "Some Description",
                                                    UserProfile = new UserProfile() { UserId = 15 }
                                                };

            var itemRepoMock = new Mock<IGenericRepository<BudgetItem>>();
            itemRepoMock.Setup(c => c.Update(item));
            itemRepoMock.Setup(c => c.SaveChanges());

            var budgetCategoryRepoMock = new Mock<IGenericRepository<BudgetCategory>>();
            var recurrentBudgetRepository = new Mock<IGenericRepository<RecurrentBudget>>();

            var instance = new BudgetService(budgetCategoryRepoMock.Object, itemRepoMock.Object, recurrentBudgetRepository.Object);

            instance.UpdateBudgetItem(item);
            itemRepoMock.VerifyAll();
        }

        [TestMethod]
        public void BudgetService_DeleteBudgetItem_By_Id()
        {
            BudgetItem item = new BudgetItem() {
                                                    Id = 6,
                                                    Amount = 21,
                                                    BudgetCategory = new BudgetCategory() { Id = 3, Name = "Dragan" },
                                                    Date = System.DateTime.MaxValue,
                                                    Description = "Some Description",
                                                    UserProfile = new UserProfile() { UserId = 15 }
                                                };

            var itemRepoMock = new Mock<IGenericRepository<BudgetItem>>();
            itemRepoMock.Setup(c => c.Delete(item.Id));
            itemRepoMock.Setup(c => c.SaveChanges());

            var budgetCategoryRepoMock = new Mock<IGenericRepository<BudgetCategory>>();
            var recurrentBudgetRepository = new Mock<IGenericRepository<RecurrentBudget>>();

            var instance = new BudgetService(budgetCategoryRepoMock.Object, itemRepoMock.Object, recurrentBudgetRepository.Object);

            instance.DeleteBudgetItem(item.Id);
            itemRepoMock.VerifyAll();
        }
    }
}
