using ApplicationServices.Services.Budget;
using Core.Data;
using Core.Services.Budget;
using Infrastructure.Data;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using HomeBudget.Helpers;

namespace HomeBudget.DependencyResolution
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory(IKernel kernel)
        {
            ninjectKernel = kernel;
            AddBindings();
        }  
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType) {  
            return controllerType == null
                ? null
                : (IController)ninjectKernel.Get(controllerType);
        }  
        private void AddBindings() {
            // put additional bindings here
            ninjectKernel.Bind<IBudgetService>().To<BudgetService>();
            ninjectKernel.Bind<IUserProfileService>().To<UserProfileService>();
            ninjectKernel.Bind<IHouseHoldService>().To<HouseHoldService>();
            ninjectKernel.Bind<HomeBudgetContext>().ToMethod(m =>  ContextFactory.DbContext);
            ninjectKernel.Bind(typeof(IGenericRepository<>)).To(typeof(GenericRepository<>));

        }
    }
}