using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Core.Services.Authentication;
using Core.Data;
using Core.Models.Authentication;
using ApplicationServices.Services.Authentication;
using Infrastructure.Data;
using DependencyResolution;

namespace HomeBudget.DependencyResolution
{
    public static class NinjectUtil
    {
        public static void SetupDependencyInjection()
        {
            IKernel kernel = new StandardKernel();
            kernel.Bind<IUserService>().To<UserService>().WithConstructorArgument("userRepository", new GenericRepository<User>());

            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}
