using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeBudget.Controllers
{
    public class HomeController : Controller
    {
        #region Fields

        public const string ACTION_MESSAGE_TEXT = "Your contact page.";

        #endregion Fields

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

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
