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
            _user = db.UserProfiles.SingleOrDefault(u => u.UserName == HttpContext.Current.User.Identity.Name);
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
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    return _user.UserId;
                }
                return 0;
            }
            set {}
        }
    }
}