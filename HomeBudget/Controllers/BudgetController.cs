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
        IHouseHoldService _houseHoldService;
        public BudgetController(IBudgetService budgetService, IUserProfileService userProfileService, IHouseHoldService houseHoldService)
        {
            _budgetService = budgetService;
            _userProfileService = userProfileService;
            _houseHoldService = houseHoldService;
        }
        //
        // GET: /Budget/
        [Authorize]
        public ActionResult Index()
        {
            int userId = _userProfileService.GetUserProfileByName(HttpContext.User.Identity.Name).Id;
            ViewBag.categories = _budgetService.GetAllBudgetCategories();

            List<int> userIds = _houseHoldService.GetAllHouseHoldMembersByUserId(userId).Select(u => u.UserId).ToList();
            userIds.Add(userId);
            ViewBag.hasHousehold = userIds.Count > 1;
            ViewBag.userId = userId;

            return View(_budgetService.GetAllBudgetItemsApproved(bi => userIds.Contains(bi.UserProfile.UserId)));
        }
        //
        // GET: /Budget/Recurrent
        [Authorize]
        public ActionResult Recurrent()
        {
            int userId = _userProfileService.GetUserProfileByName(HttpContext.User.Identity.Name).Id;
            ViewBag.categories = _budgetService.GetAllBudgetCategories();

            List<int> userIds = _houseHoldService.GetAllHouseHoldMembersByUserId(userId).Select(u => u.UserId).ToList();
            userIds.Add(userId);

            ViewBag.hasHousehold = userIds.Count > 1;
            ViewBag.userId = userId;

            return View("Index", _budgetService.GetAllRecurrentBudgetItems(bi => userIds.Contains(bi.UserProfile.UserId)));
        }
        //
        // GET: /Budget/NoneRecurrent
        [Authorize]
        public ActionResult NonRecurrent()
        {
            int userId = _userProfileService.GetUserProfileByName(HttpContext.User.Identity.Name).Id;
            ViewBag.categories = _budgetService.GetAllBudgetCategories();
            
            List<int> userIds = _houseHoldService.GetAllHouseHoldMembersByUserId(userId).Select(u => u.UserId).ToList();
            userIds.Add(userId);

            ViewBag.hasHousehold = userIds.Count > 1;
            ViewBag.userId = userId;

            return View("Index", _budgetService.GetAllNonRecurrentBudgetItems(bi => userIds.Contains(bi.UserProfile.UserId)));
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
            int userId = _userProfileService.GetUserProfileByName(HttpContext.User.Identity.Name).Id;
            ViewBag.userId = userId;

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
            ViewBag.Categories = _budgetService.GetAllBudgetCategories()
                .Select(x => new { value = x.Id, text = x.Name });
            return View();
        }

        //
        // POST: /Budget/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(BudgetItem budgetitem)
        {
            budgetitem.UserProfile = _userProfileService.GetUserProfileByName(HttpContext.User.Identity.Name);
            if (ModelState.IsValid)
            {
                _budgetService.InsertBudgetItem(budgetitem);
                return RedirectToAction("Index");
            }
            ViewBag.Categories = _budgetService.GetAllBudgetCategories()
                .Select(x => new { value = x.Id, text = x.Name });
            return View(budgetitem);
        }

        //
        // GET: /Budget/Edit/5
        [Authorize]
        public ActionResult Edit(int id = 0)
        {
            ViewBag.Categories = _budgetService.GetAllBudgetCategories()
                .Select(x => new { value = x.Id, text = x.Name });

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
            int userId = _userProfileService.GetUserProfileByName(HttpContext.User.Identity.Name).Id;
            ViewBag.categoryList = new SelectList(_budgetService.GetAllBudgetCategories()
               .Select(x => new { value = x.Id, text = x.Name }),
               "value", "text");
            budgetitem.UserProfile = new Core.Models.Authentication.UserProfile { UserId = userId};
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