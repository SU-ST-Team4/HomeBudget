using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Models.Budget;
using Infrastructure.Data;
using Core.Services.Budget;

namespace HomeBudget.Controllers
{
    public class BudgetCategoryController : Controller
    {
        IBudgetService _budgetService;
        public BudgetCategoryController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        //
        // GET: /BudgetCategory/
        [Authorize]
        public ActionResult Index()
        {
            return View(_budgetService.GetAllBudgetCategories());
        }

        //
        // GET: /BudgetCategory/Details/5
        [Authorize]
        public ActionResult Details(int id = 0)
        {
            BudgetCategory budgetcategory = _budgetService.GetAllBudgetCategories(bc => bc.Id == id).First();
            if (budgetcategory == null)
            {
                return HttpNotFound();
            }
            return View(budgetcategory);
        }

        //
        // GET: /BudgetCategory/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /BudgetCategory/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(BudgetCategory budgetcategory)
        {
            if (ModelState.IsValid)
            {
                _budgetService.InsertBudgetCategory(budgetcategory);
                return RedirectToAction("Index");
            }

            return View(budgetcategory);
        }

        //
        // GET: /BudgetCategory/Edit/5
        [Authorize]
        public ActionResult Edit(int id = 0)
        {
            BudgetCategory budgetcategory = _budgetService.GetAllBudgetCategories(bc => bc.Id == id).First();
            if (budgetcategory == null)
            {
                return HttpNotFound();
            }
            return View(budgetcategory);
        }

        //
        // POST: /BudgetCategory/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(BudgetCategory budgetcategory)
        {
            if (ModelState.IsValid)
            {
                _budgetService.UpdateBudgetCategory(budgetcategory);
                return RedirectToAction("Index");
            }
            return View(budgetcategory);
        }

        //
        // GET: /BudgetCategory/Delete/5
        [Authorize]
        public ActionResult Delete(int id = 0)
        {
            return RedirectToAction("Index");
            /*
            BudgetCategory budgetcategory = db.BudgetCategories.Find(id);
            if (budgetcategory == null)
            {
                return HttpNotFound();
            }
            return View(budgetcategory);
            */
        }

        //
        // POST: /BudgetCategory/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            return RedirectToAction("Index");
            /*
            BudgetCategory budgetcategory = db.BudgetCategories.Find(id);
            db.BudgetCategories.Remove(budgetcategory);
            db.SaveChanges();
            return RedirectToAction("Index");
            */
        }
    }
}