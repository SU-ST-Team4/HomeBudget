using Core.Models.Budget;
using Core.Models.Authentication;
using Core.Services.Budget;
using HomeBudget.Helpers.UserProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeBudget.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBudgetService _budgetService;
        private readonly IHouseHoldService _houseHoldService;
        private readonly IUserProfileService _userProfileService;

        public HomeController(IBudgetService budgetService, IHouseHoldService houseHoldService, IUserProfileService userProfileService)
        {
            _budgetService = budgetService;
            _houseHoldService = houseHoldService;
            _userProfileService = userProfileService;
        }


        public const string ACTION_MESSAGE_TEXT = "Your contact page.";

        [Authorize]
        public ActionResult Dashboard()
        {
            int userId = _userProfileService.GetUserProfileByName(HttpContext.User.Identity.Name).Id;

            List<int> userIds = _houseHoldService.GetAllHouseHoldMembersByUserId(userId).Select(u => u.UserId).ToList();
            userIds.Add(userId);

            var current = _budgetService.GetLastNMonthBudgetPreviewByUserIds(userIds, 1);
            var previous = _budgetService.GetLastNMonthBudgetPreviewByUserIds(userIds, 2);
            var bprevious = _budgetService.GetLastNMonthBudgetPreviewByUserIds(userIds, 3);

            decimal cost = current.Cost;
            decimal balance = current.Balance;
            decimal income = cost + balance;
            ViewBag.currentIncome = income;
            ViewBag.currentCost = cost;
            ViewBag.currentBalance = balance;
            
            cost = previous.Cost - cost;
            balance = previous.Balance - balance;
            income = cost + balance;
            ViewBag.previousIncome = income;
            ViewBag.previousCost = cost;
            ViewBag.previousBalance = balance;

            cost = bprevious.Cost - cost;
            balance = bprevious.Balance - balance;
            income = cost + balance;
            ViewBag.bpreviousIncome = income;
            ViewBag.bpreviousCost = cost;
            ViewBag.bpreviousBalance = balance;



            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = ACTION_MESSAGE_TEXT;

            return View();
        }
    }
}
