using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Core.Models.Budget;
using Infrastructure.Data;
using Core.Services.Budget;
using HomeBudget.Helpers.UserProfile;
using Core.Models.Authentication;
using HomeBudget.Models;

namespace HomeBudget.Controllers
{
    public class HouseholdController : Controller
    {
        IHouseHoldService _houseService;
        IUserProfileService _userProfileService;
        public HouseholdController(IHouseHoldService houseHoldService, IUserProfileService userProfileService)
        {
            _houseService = houseHoldService;
            _userProfileService = userProfileService;
        }
        //
        // GET: /HouseHold/
        [Authorize]
        public ActionResult Index()
        {
            int userId = _userProfileService.GetUserProfileByName(HttpContext.User.Identity.Name).Id;
            return View(_houseService.GetAllApprovedHouseHoldsByUserId(userId));
        }

        [Authorize]
        public ActionResult Requests()
        {
            int userId = _userProfileService.GetUserProfileByName(HttpContext.User.Identity.Name).Id;
            return View(_houseService.GetAllNotApprovedHouseHoldRequestsByUserId(userId));
        }


        //
        // GET: /HouseHold/Create
        [Authorize]
        public ActionResult Create()
        {

            return View();
        }

        //
        // POST: /HouseHold/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(HouseHoldRequestModel householdRequest)
        {
            HouseHold household = new HouseHold();
            household.First = _userProfileService.GetUserProfileByName(HttpContext.User.Identity.Name);
            household.Second = _userProfileService.GetUserProfileByName(householdRequest.UserName);
            household.RequestMessage = householdRequest.RequestMessage;

            try
            {
                if (household.Second.UserId == household.First.UserId)
                {
                    throw new Exception("You can't add your self");
                }
                List<int> userIds = _houseService.GetAllHouseHoldMembersByUserId(household.First.UserId).Select(u => u.UserId).ToList();
                if (userIds.Contains(household.Second.UserId)) {
                    throw new Exception("User already added");
                }

                ValidateModel(household);
                _houseService.RequestHouseHold(household);
                return RedirectToAction("Index");
            }
            catch
            {
            }
            return View(householdRequest);
        }

                //
        // GET: /HouseHold/Delete/5
        [Authorize]
        public ActionResult Delete(int id = 0)
        {
            int userId = _userProfileService.GetUserProfileByName(HttpContext.User.Identity.Name).Id;
            List<HouseHold> households = _houseService.GetAllApprovedHouseHoldsByUserId(userId);
            bool found = false;

            foreach (HouseHold household in households)
            {
                if (household.Id == id)
                {
                    found = true;
                    break;
                }
            }
            if (found)
            {
                _houseService.RemoveUserFromHouseHold(id);
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Approve(int id)
        {
            int userId = _userProfileService.GetUserProfileByName(HttpContext.User.Identity.Name).Id;
            List<HouseHold> households = _houseService.GetAllNotApprovedHouseHoldRequestsByUserId(userId);
            bool found = false;

            foreach (HouseHold household in households)
            {
                if (household.Id == id)
                {
                    found = true;
                    break;
                }
            }
            if (found)
            {
                _houseService.ApproveHouseHoldRequest(id);
                return RedirectToAction("Requests");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public JsonResult FindUser(string userName)
        {
            UserProfile userProfile = _userProfileService.GetUserProfileByName(userName);
            return Json(new
            {
                Status = userProfile != null,
                UserId = userProfile != null? userProfile.Id : 0
            });
        }
    }
}