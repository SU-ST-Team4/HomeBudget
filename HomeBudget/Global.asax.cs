﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Threading;
using System.Web.Routing;
using System.Web.Security;
using Core.Models.Authentication;
using Infrastructure.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Configuration;
using HomeBudget.DependencyResolution;

namespace HomeBudget
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            DependencyResolution.NinjectUtil.SetupDependencyInjection();
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory(DependencyResolution.NinjectUtil.kernel));

            using (var ctx = new HomeBudgetContext())
            {
                ctx.Database.Initialize(true);
            }
    
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }
    }
}