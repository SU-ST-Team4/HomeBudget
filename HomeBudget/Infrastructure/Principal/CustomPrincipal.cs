using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Principal;
using Core.Models.Authentication;

namespace HomeBudget.Infrastructure.Principal
{
    public class CustomPrincipal : ICustomPrincipal
    {
        public CustomPrincipal(UserExample user)
        {
            this.Identity = user;
        }

        #region ICustomPrincipal Implementation

        public IIdentity Identity { get; private set; }

        public string FirstName { get { return (Identity as UserExample).FirstName; } }

        public string Lastname { get { return (Identity as UserExample).LastName; } }

        public bool IsInRole(string role)
        {
            return true;
        }


        #endregion ICustomPrincipal Implementation

    }
}
