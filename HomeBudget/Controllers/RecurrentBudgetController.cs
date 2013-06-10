using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Models.Budget;
using Core.Services.Budget;
using HomeBudget.Helpers.UserProfile;

namespace HomeBudget.Controllers
{
    public class RecurrentBudgetController : Controller
    {
        IBudgetService _budgetService;
        public RecurrentBudgetController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }
        //
        // GET: /RecurrentBudget/

        public ActionResult Index()
        {
            int userId = CurrentUser.Get().Id;
            return View(_budgetService.GetAllRecurrentBudgets(i => i.UserProfile.UserId == userId));
        }

        //
        // GET: /RecurrentBudget/Details/5

        public ActionResult Details(int id = 0)
        {
            RecurrentBudget recurrentbudget = _budgetService.GetAllRecurrentBudgets(i => i.Id == id).First();
            if (recurrentbudget == null)
            {
                return HttpNotFound();
            }
            return View(recurrentbudget);
        }

        //
        // GET: /RecurrentBudget/Create

        public ActionResult Create()
        {
            ViewBag.BudgetCategory_Id = new SelectList(_budgetService.GetAllBudgetCategories()
                .Select(x => new { value = x.Id, text = x.Name }),
                "value", "text");
            return View();
        }

        //
        // POST: /RecurrentBudget/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RecurrentBudget recurrentbudget)
        {
            recurrentbudget.UserProfile = _budgetService.GetUserProfile("recurrentBudget", CurrentUser.Get().Name);
            recurrentbudget.BudgetCategory = _budgetService.GetCategory("recurrentBudget", 2);
            if (ModelState.IsValid)
            {
                _budgetService.InsertRecurrentBudget(recurrentbudget);
                return RedirectToAction("Index");
            }
            ViewBag.BudgetCategory_Id = new SelectList(_budgetService.GetAllBudgetCategories()
                .Select(x => new { value = x.Id, text = x.Name }),
                "value", "text");

            return View(recurrentbudget);
        }

        //
        // GET: /RecurrentBudget/Edit/5

        public ActionResult Edit(int id = 0)
        {
            RecurrentBudget recurrentbudget = _budgetService.GetAllRecurrentBudgets(i => i.Id == id).First();
            if (recurrentbudget == null)
            {
                return HttpNotFound();
            }
            ViewBag.BudgetCategory_Id = new SelectList(_budgetService.GetAllBudgetCategories()
               .Select(x => new { value = x.Id, text = x.Name }),
               "value", "text", recurrentbudget.BudgetCategory.Id);
            return View(recurrentbudget);
        }

        //
        // POST: /RecurrentBudget/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RecurrentBudget recurrentbudget)
        {
            recurrentbudget.UserProfile = _budgetService.GetUserProfile("recurrentBudget", CurrentUser.Get().Name);
            recurrentbudget.BudgetCategory = _budgetService.GetCategory("recurrentBudget", 2);
            if (ModelState.IsValid)
            {
                _budgetService.UpdateRecurrentBudget(recurrentbudget);
                return RedirectToAction("Index");
            }
            ViewBag.BudgetCategory_Id = new SelectList(_budgetService.GetAllBudgetCategories()
               .Select(x => new { value = x.Id, text = x.Name }),
               "value", "text", recurrentbudget.BudgetCategory.Id);
            return View(recurrentbudget);
        }

        //
        // GET: /RecurrentBudget/Delete/5

        public ActionResult Delete(int id = 0)
        {
            RecurrentBudget recurrentbudget = _budgetService.GetAllRecurrentBudgets(i => i.Id == id).First();
            if (recurrentbudget == null)
            {
                // no delete currently
                return HttpNotFound();
            }
            return View(recurrentbudget);
        }

        //
        // POST: /RecurrentBudget/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            return RedirectToAction("Index");
        }
    }
}