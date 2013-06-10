using Core.Models.Budget;
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
        IBudgetService _budgetService;
        public HomeController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }


        public const string ACTION_MESSAGE_TEXT = "Your contact page.";

        [Authorize]
        public ActionResult Dashboard()
        {
            ViewBag.current = _budgetService.GetLastNMonthBudgetPreviewByUserId(CurrentUser.Get().Id, 1);
            ViewBag.previous = _budgetService.GetLastNMonthBudgetPreviewByUserId(CurrentUser.Get().Id, 2);
            ViewBag.bprevious = _budgetService.GetLastNMonthBudgetPreviewByUserId(CurrentUser.Get().Id, 3);
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
