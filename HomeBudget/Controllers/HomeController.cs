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

        public HomeController(IBudgetService budgetService, IHouseHoldService houseHoldService)
        {
            _budgetService = budgetService;
            _houseHoldService = houseHoldService;
        }


        public const string ACTION_MESSAGE_TEXT = "Your contact page.";

        [Authorize]
        public ActionResult Dashboard()
        {
            int userId = CurrentUser.Get().Id;

            List<int> userIds = _houseHoldService.GetAllHouseHoldMembersByUserId(userId).Select(u => u.UserId).ToList();
            userIds.Add(userId);

            ViewBag.current = _budgetService.GetLastNMonthBudgetPreviewByUserIds(userIds, 1);
            ViewBag.previous = _budgetService.GetLastNMonthBudgetPreviewByUserIds(userIds, 2);
            ViewBag.bprevious = _budgetService.GetLastNMonthBudgetPreviewByUserIds(userIds, 3);
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
