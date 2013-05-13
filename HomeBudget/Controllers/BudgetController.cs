using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Models.Budget;
using Infrastructure.Data;
using HomeBudget.Helpers.UserProfile;
using Core.Services.Budget;

namespace HomeBudget.Controllers
{
    public class BudgetController : Controller
    {
        IBudgetService _budgetService;
        public BudgetController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }
        //
        // GET: /Budget/
        [Authorize]
        public ActionResult Index()
        {
            int userId = CurrentUser.Get().Id;
            ViewBag.categories = _budgetService.GetAllBudgetCategories();

            return View(_budgetService.GetAllBudgetItems(bi => bi.UserId == userId));
        }

        //
        // GET: /Budget/Details/5
        [Authorize]
        public ActionResult Details(int id = 0)
        {
            BudgetItem budgetitem = _budgetService.GetAllBudgetItems(bi => bi.Id == id).First();
            if (budgetitem == null)
            {
                return HttpNotFound();
            }
            return View(budgetitem);
        }

        //
        // GET: /Budget/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.categoryList = new SelectList(_budgetService.GetAllBudgetCategories()
                .Select(x => new { value = x.Id, text = x.Name }),
                "value", "text");
            return View();
        }

        //
        // POST: /Budget/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(BudgetItem budgetitem)
        {
            ViewBag.categoryList = new SelectList(_budgetService.GetAllBudgetCategories()
               .Select(x => new { value = x.Id, text = x.Name }),
               "value", "text");

            budgetitem.UserId = CurrentUser.Get().Id;
            if (ModelState.IsValid)
            {
                _budgetService.InsertBudgetItem(budgetitem);
                return RedirectToAction("Index");
            }

            return View(budgetitem);
        }

        //
        // GET: /Budget/Edit/5
        [Authorize]
        public ActionResult Edit(int id = 0)
        {
            ViewBag.categoryList = new SelectList(_budgetService.GetAllBudgetCategories()
               .Select(x => new { value = x.Id, text = x.Name }),
               "value", "text");
            BudgetItem budgetitem = _budgetService.GetAllBudgetItems(bi => bi.Id == id).First();
            if (budgetitem == null)
            {
                return HttpNotFound();
            }
            return View(budgetitem);
        }

        //
        // POST: /Budget/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(BudgetItem budgetitem)
        {
            ViewBag.categoryList = new SelectList(_budgetService.GetAllBudgetCategories()
               .Select(x => new { value = x.Id, text = x.Name }),
               "value", "text");
            budgetitem.UserId = CurrentUser.Get().Id;
            if (ModelState.IsValid)
            {
                _budgetService.UpdateBudgetItem(budgetitem);
                return RedirectToAction("Index");
            }
            return View(budgetitem);
        }

        //
        // GET: /Budget/Delete/5
        [Authorize]
        public ActionResult Delete(int id = 0)
        {
            _budgetService.DeleteBudgetItem(id);
            return RedirectToAction("Index");
        }

        //
        // POST: /Budget/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            _budgetService.DeleteBudgetItem(id);
            return RedirectToAction("Index");
        }
    }
}