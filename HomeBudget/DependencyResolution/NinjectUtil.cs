using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Core.Data;
using Core.Models.Authentication;
using Infrastructure.Data;
using DependencyResolution;
using Core.Services.Budget;
using ApplicationServices.Services.Budget;

namespace HomeBudget.DependencyResolution
{
    public static class NinjectUtil
    {
        public static IKernel kernel = new StandardKernel();
        public static void SetupDependencyInjection()
        {
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}
