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
        protected override IController GetControllerInstance(RequestContext requestContext,              Type controllerType) {  
            return controllerType == null
                ? null
                : (IController)ninjectKernel.Get(controllerType);
        }  
        private void AddBindings() {
            // put additional bindings here
            ninjectKernel.Bind<IBudgetService>().To<BudgetService>();
            ninjectKernel.Bind(typeof(IGenericRepository<>)).To(typeof(GenericRepository<>));
        }
    }
}