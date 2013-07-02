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
using HomeBudget.Helpers;

namespace HomeBudget.Controllers
{
    public class RecurrentBudgetController : Controller
    {
        private readonly IBudgetService _budgetService;
        private readonly IUserProfileService _userProfileService;
        
        public RecurrentBudgetController(IBudgetService budgetService, IUserProfileService userProfileService)
        {
            _budgetService = budgetService;
            _userProfileService = userProfileService;
        }
        //
        // GET: /RecurrentBudget/
        [Authorize]
        public ActionResult Index()
        {
            int userId = _userProfileService.GetUserProfileByName(HttpContext.User.Identity.Name).UserId;
            return View(_budgetService.GetAllRecurrentBudgets(i => i.UserProfile.UserId == userId));
        }

        //
        // GET: /RecurrentBudget/Details/5
        [Authorize]
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
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Categories = _budgetService.GetAllBudgetCategories()
                 .Select(x => new { value = x.Id, text = x.Name });
            return View();
        }

        //
        // POST: /RecurrentBudget/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(RecurrentBudget recurrentbudget)
        {
            string userName = HttpContext.User.Identity.Name;
            recurrentbudget.UserProfile = _userProfileService.GetUserProfileByName(userName);
            //recurrentbudget.BudgetCategory_Id = 1;
            if (ModelState.IsValid)
            {
                _budgetService.InsertRecurrentBudget(recurrentbudget);
                return RedirectToAction("Index");
            }
            ViewBag.Categories = _budgetService.GetAllBudgetCategories()
                .Select(x => new { value = x.Id, text = x.Name });

            return View(recurrentbudget);
        }

        //
        // GET: /RecurrentBudget/Edit/5
        [Authorize]
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
        [Authorize]
        public ActionResult Edit(RecurrentBudget recurrentbudget)
        {
            string userName =   HttpContext.User.Identity.Name;
            recurrentbudget.UserProfile = _userProfileService.GetUserProfileByName(userName);
            recurrentbudget.BudgetCategory = _budgetService.GetAllBudgetCategories().First(c => c.Id == 2);
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
        [Authorize]
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
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            return RedirectToAction("Index");
        }
    }
}