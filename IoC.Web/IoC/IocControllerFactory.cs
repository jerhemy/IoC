using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace IoCContainer
{
    public class IocControllerFactory : DefaultControllerFactory
    {
        private readonly IocContainer _container;

        public IocControllerFactory()
        {
        }

        public IocControllerFactory(IocContainer container)
        {
            _container = container;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
                return null;

            IController _controller = null;

            foreach (Type tinterface in controllerType.GetInterfaces())
            {
                try
                {
                    _controller = (IController)_container.Resolve(tinterface);             
                }
                catch (Exception ex)
                {

                }
            }

            return _controller;
            //return (IController)_container.Resolve(controllerType);
        }

        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            var controller = base.CreateController(requestContext, controllerName);

            return controller;
        }

        protected override System.Web.SessionState.SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, Type controllerType)
        {
            return base.GetControllerSessionBehavior(requestContext, controllerType);
        }
    }
}
