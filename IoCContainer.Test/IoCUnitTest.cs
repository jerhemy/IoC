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
        public void Resolve_Registered_Type()
        {
            var container = new IocContainer();
            container.Register<ILogger, Logger>();
            var logger = container.Resolve<ILogger>();
            Assert.IsInstanceOfType(logger, typeof(Logger));
        }

        [TestMethod]
        public void Resolve_Registered_Type_Class()
        {
            var container = new IocContainer();
            container.Register<ILogger, Logger>();
            container.Register<IUserController, UserController>();

            var userController = container.Resolve<IUserController>();
            Assert.IsInstanceOfType(userController, typeof(UserController));
        }


        [TestMethod]
        [ExpectedException(typeof(TypeLoadException))]
        public void Resolve_UnRegistered_Type_Throws_TypeLoadException()
        {
            var container = new IocContainer();
            var logger = container.Resolve<ILogger>();
        }

        [TestMethod]
        [ExpectedException(typeof(TypeLoadException))]
        public void Resolve_UnRegistered_Injection_Class_Throws_TypeLoadExceptionn()
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
        public void Register_Invalid_Interface_Throws_ArgumentException()
        {
            var container = new IocContainer();
            container.Register<Logger, Logger>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Register_Invalid_Class_Throws_ArgumentException()
        {
            var container = new IocContainer();
            container.Register<ILogger, ILogger>();
        }

    }
}
