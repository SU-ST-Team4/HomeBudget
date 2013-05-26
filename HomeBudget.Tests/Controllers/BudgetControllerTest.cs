using Core.Models.Budget;
using Core.Services.Budget;
using HomeBudget.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Linq;

namespace HomeBudget.Tests.Controllers
{
    [TestClass]
    public class BudgetControllerTest
    {
        [TestMethod]
        public void BudgetController_Index()
        {
            var budgetServiceMock = new Mock<IBudgetService>();

            budgetServiceMock.Setup(bs => bs.GetAllBudgetCategories(It.IsAny<Expression<Func<BudgetCategory, bool>>>()))
                             .Returns(new List<BudgetCategory>() { new BudgetCategory(){Id = 5}});
            budgetServiceMock.Setup(bs => bs.GetAllBudgetItems(It.IsAny<Expression<Func<BudgetItem, bool>>>()))
                             .Returns(new List<BudgetItem>()
                                     {
                                         new BudgetItem(){Id = 20},
                                         new BudgetItem(){Id = 21}
                                     });

            ViewResult result = new BudgetController(budgetServiceMock.Object).Index() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsTrue((result.ViewBag.categories.Count == 1));
            Assert.IsTrue((result.ViewBag.categories[0].Id == 5));
            Assert.IsTrue((result.Model as IEnumerable<Core.Models.Budget.BudgetItem>).ToList().Count == 2);
            Assert.IsTrue((result.Model as IEnumerable<Core.Models.Budget.BudgetItem>).ToList()[0].Id == 20);
            Assert.IsTrue((result.Model as IEnumerable<Core.Models.Budget.BudgetItem>).ToList()[1].Id == 21);

            budgetServiceMock.Verify(bs => bs.GetAllBudgetCategories(It.IsAny<Expression<Func<BudgetCategory, bool>>>()), Times.Exactly(1));
            budgetServiceMock.Verify(bs => bs.GetAllBudgetItems(It.IsAny<Expression<Func<BudgetItem, bool>>>()), Times.Exactly(1));
        }
        [TestMethod]
        public void BudgetController_Create_Valid_BudgetItem()
        {
            BudgetItem bi1 = new BudgetItem() 
            { 
                Amount = 10, 
                Id = 5, 
                BudgetCategory = new BudgetCategory() 
                { 
                    Id = 2,
                    Name = "Grudi"
                },
                Date = DateTime.Now,
                Description = "Kvo puk",
                UserId = 0
            };

            var budgetServiceMock = new Mock<IBudgetService>();
            budgetServiceMock.Setup(bs => bs.GetAllBudgetCategories(It.IsAny<Expression<Func<BudgetCategory, bool>>>()))
                             .Returns(new List<BudgetCategory>() { new BudgetCategory() { Id = 5 } });
            budgetServiceMock.Setup(bs => bs.InsertBudgetItem(It.Is<BudgetItem>(bi => bi.Id == 5)))
                             .Returns(-20);

            var result = new BudgetController(budgetServiceMock.Object).Create(bi1);
            Assert.AreEqual("Index", (result as RedirectToRouteResult).RouteValues["action"]);
            Assert.IsNull((result as RedirectToRouteResult).RouteValues["controller"]);

            budgetServiceMock.Verify(bs => bs.InsertBudgetItem(It.IsAny<BudgetItem>()), Times.Exactly(1));
            budgetServiceMock.Verify(bs => bs.GetAllBudgetCategories(It.IsAny<Expression<Func<BudgetCategory, bool>>>()), Times.Exactly(1));
        }

        [TestMethod]
        public void BudgetController_Create_Invalid_BudgetItem()
        {
            //not valid state
            BudgetItem bi2 = new BudgetItem();
            var budgetServiceMock = new Mock<IBudgetService>();
            budgetServiceMock.Setup(bs => bs.GetAllBudgetCategories(It.IsAny<Expression<Func<BudgetCategory, bool>>>()))
                 .Returns(new List<BudgetCategory>() { new BudgetCategory() { Id = 5 } });

            var controller = new BudgetController(budgetServiceMock.Object);
            controller.ViewData.ModelState.AddModelError("Key", "ErrorMessage");
            var result = controller.Create(bi2);
            Assert.IsTrue((result as ViewResult).ViewName == String.Empty);
                 
            budgetServiceMock.Verify(bs => bs.GetAllBudgetCategories(It.IsAny<Expression<Func<BudgetCategory, bool>>>()), Times.Exactly(1));
            budgetServiceMock.Verify(bs => bs.InsertBudgetItem(It.IsAny<BudgetItem>()), Times.Never());
        }

        [TestMethod]
        public void BudgetController_Edit_With_Int_Param()
        {
                
        }
    }
}
