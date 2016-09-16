using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IoC.Web.Controllers;
using IoC.Web.Interfaces;
using IoC.Web.Models;
using IoCContainer;

namespace IoC.Web.App_Start
{
    public static class Bootstrap
    {
        public static void Configure(IocContainer container)
        {
            container.Register<ILogger, Logger>();
            container.Register<IHomeController, HomeController>();
        }
    }
}