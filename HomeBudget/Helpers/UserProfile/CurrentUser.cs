using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace HomeBudget.Helpers.UserProfile
{
    public class CurrentUser
    {
        private Core.Models.Authentication.UserProfile _user = null;
        private HomeBudgetContext db;
        private static CurrentUser _instace;

        public CurrentUser() {
            db = new HomeBudgetContext();
            if (HttpContext.Current != null && HttpContext.Current.User != null)
            {
                _user = db.UserProfiles.SingleOrDefault(u => u.UserName == HttpContext.Current.User.Identity.Name);
            }
            else
            {
                _user = null;
            }
        }
        public static CurrentUser Get()
        {
            if (_instace == null)
            {
                _instace = new CurrentUser();
            }
            return _instace;
        }
        public int Id
        {
            get {
                if (HttpContext.Current != null && 
                    HttpContext.Current.User != null && 
                    HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    return _user.UserId;
                }
                return 0;
            }
            set {}
        }
        public string Name
        {
            get
            {
                if (HttpContext.Current != null &&
                    HttpContext.Current.User != null &&
                    HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    return _user.UserName;
                }
                return "";
            }
            set { }
        }
       
    }



}