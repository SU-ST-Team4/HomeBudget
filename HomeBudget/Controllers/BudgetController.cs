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
using HomeBudget.Helpers;
using Core.Services.Budget;

namespace HomeBudget.Controllers
{
    public class BudgetController : Controller
    {
        IBudgetService _budgetService;
        IUserProfileService _userProfileService;
        public BudgetController(IBudgetService budgetService, IUserProfileService userProfileService)
        {
            _budgetService = budgetService;
            _userProfileService = userProfileService;
        }
        //
        // GET: /Budget/
        [Authorize]
        public ActionResult Index()
        {
            int userId = CurrentUser.Get().Id;
            ViewBag.categories = _budgetService.GetAllBudgetCategories();

            return View(_budgetService.GetAllBudgetItems(bi => bi.UserProfile.UserId == userId));
        }
        //
        // GET: /Budget/Recurrent
        [Authorize]
        public ActionResult Recurrent()
        {
            int userId = CurrentUser.Get().Id;
            ViewBag.categories = _budgetService.GetAllBudgetCategories();

            return View("Index", _budgetService.GetAllRecurrentBudgetItems(bi => bi.UserProfile.UserId == userId));
        }
        //
        // GET: /Budget/NoneRecurrent
        [Authorize]
        public ActionResult NonRecurrent()
        {
            int userId = CurrentUser.Get().Id;
            ViewBag.categories = _budgetService.GetAllBudgetCategories();

            return View("Index", _budgetService.GetAllNonRecurrentBudgetItems(bi => bi.UserProfile.UserId == userId));
        }

        //
        // GET: /Budget/Details/5
        [Authorize]
        public ActionResult Details(int id = 0)
        {
            BudgetItem budgetitem = _budgetService.GetAllRecurrentBudgetItems(bi => bi.Id == id).First();
            if (budgetitem == null)
            {
                return HttpNotFound();
            }
            return View(budgetitem);
        }

        //
        // GET: /Budget/Approve/5
        [Authorize]
        public ActionResult Approve(int id)
        {
            _budgetService.ApproveRecurrentBudgetItem(id, true);

            // unit test UNfriendly
            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }

        //
        // GET: /Budget/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.BudgetCategory_Id = new SelectList(_budgetService.GetAllBudgetCategories()
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
            ViewBag.BudgetCategory_Id = new SelectList(_budgetService.GetAllBudgetCategories()
                .Select(x => new { value = x.Id, text = x.Name }),
                "value", "text");

            string userName = CurrentUser.Get().Name;

            budgetitem.UserProfile = _userProfileService.GetUserProfileByName(userName);
            budgetitem.BudgetCategory = _budgetService.GetAllBudgetCategories().First(c => c.Id == 2);
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
            budgetitem.UserProfile = new Core.Models.Authentication.UserProfile { UserId = CurrentUser.Get().Id};
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