using System;
using ClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedLibrary;

namespace IoCContainer.Test
{
    [TestClass]
    public class IoCUnitTest
    {
        [TestMethod]
        public void Resolve_RegisteredType()
        {
            var container = new IocContainer();
            container.Register<ILogger, Logger>();
            var logger = container.Resolve<ILogger>();
            Assert.IsInstanceOfType(logger, typeof(Logger));
        }

        [TestMethod]
        [ExpectedException(typeof(TypeLoadException))]
        public void Resolve_UnRegisteredType_ThrowsException()
        {
            var container = new IocContainer();
            var logger = container.Resolve<ILogger>();
        }

        [TestMethod]
        public void Resolve_Class_DependencyInjection()
        {
            var container = new IocContainer();
            container.Register<ILogger, Logger>();
            container.Register<IUserController, UserController>();

            var userController = container.Resolve<IUserController>();
            Assert.IsInstanceOfType(userController, typeof(UserController));
        }

        [TestMethod]
        [ExpectedException(typeof(TypeLoadException))]
        public void Resolve_UnRegisteredType_Class_DependencyInjection()
        {
            var container = new IocContainer();
            container.Register<IUserController, UserController>();

            var userController = container.Resolve<IUserController>();
            Assert.IsInstanceOfType(userController, typeof(UserController));
        }

        [TestMethod]
        public void Resolve_Singleton_Class()
        {
            var container = new IocContainer();
            container.Register<ILogger, Logger>();
            container.Register<IUserController, UserController>(LifeCycle.Singleton);

            var userController1 = container.Resolve<IUserController>();
            var userController2 = container.Resolve<IUserController>();

            Assert.AreSame(userController1, userController2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Register_Invalid_Interface()
        {
            var container = new IocContainer();
            container.Register<Logger, Logger>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Register_Invalid_Class()
        {
            var container = new IocContainer();
            container.Register<ILogger, ILogger>();
        }

    }
}
