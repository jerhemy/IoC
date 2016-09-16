using System;
using System.Collections.Generic;
using System.Reflection;

namespace IoCContainer
{
    public class IocContainer
    {

        private readonly Dictionary<Type, Type> _types = new Dictionary<Type, Type>();
        private readonly Dictionary<Type, object> _singletonTypes = new Dictionary<Type, object>();

        //Register Type with Default Lifecycle
        public void Register<TType, TClass>() where TClass : class
        {
            if(!typeof(TType).IsInterface) {

                throw new ArgumentException(string.Format("{0} is not an Interface", typeof(TType)));
            }

            if (!typeof(TClass).IsClass)
            {
                throw new ArgumentException(string.Format("{0} is not a Class", typeof(TClass)));
            }

            _types[typeof(TType)] = typeof(TClass);
        }

        //Register Type with User Defined LifeCylce
        public void Register<TType, TClass>(LifeCycle type) where TClass : class
        {
            switch (type)
            {
                case LifeCycle.Singleton:
                    _types[typeof(TType)] = typeof(TClass);
                    _singletonTypes[typeof(TType)] = Resolve<TType>();
                    break;
                default:
                    Register<TType, TClass>();
                    break;
            }
        }

        public object Resolve(Type contract)
        {
            if (!_singletonTypes.ContainsKey(contract) && !_types.ContainsKey(contract))
            {
                throw new TypeLoadException(string.Format("{0} not Registered.", contract));
            }

            if (_singletonTypes.ContainsKey(contract))
            {
                return _singletonTypes[contract];
            }
            else
            {
                Type implementation = _types[contract];
                ConstructorInfo constructor = implementation.GetConstructors()[0];
                ParameterInfo[] constructorParameters = constructor.GetParameters();
                if (constructorParameters.Length == 0)
                {
                    return Activator.CreateInstance(implementation);
                }
                List<object> parameters = new List<object>(constructorParameters.Length);
                foreach (ParameterInfo parameterInfo in constructorParameters)
                {
                    parameters.Add(Resolve(parameterInfo.ParameterType));
                }
                return constructor.Invoke(parameters.ToArray());
            }
        }
        
        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

    }
}
