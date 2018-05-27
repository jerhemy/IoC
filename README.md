# IoC
Custom IoC Interview Pratical

## Requirements
* Write an IoC (Inversion of Control) container, also known as a Dependency Injection container.
* The container must allow you to register types.
  * Example: container.Register<ICalculator, Calculator>()
* The container must be aware and control object lifecycle for transient objects (a new instance is created for each request), and singleton objects (the same instance is returned for each request). 
  * Example: container.Register<ICalculator, Calculator>(LifestyleType.Transient), or container.Register<ICalculator, Calculator (LifestyleType.Singleton)
  
* The default lifecycle for an object should be transient

* You must be able to resolve registered types through the container
  * Example: container.Resolve<ICalculator>()
  
* If you try to resolve a type that the container is unaware of, it should throw an informative exception.

* When resolving from the container for a registered type, if that type has constructor arguments which are also registered types, the container should inject the instances into the constructor appropriately (this is where dependency injection applies).
  * Example Constructor: public UsersController(ICalculator calculator, IEmailService emailService). If all 3 types for the controller, repository, and email service are registered you should be able to resolve an instance of the UsersController.
  
* You must write tests for all of this behavior using xUnit.

* With a simple new ASP.NET MVC Web Application, wire things up so your Controllers can be constructed using your containers. There are many well documented ways to accomplish this with other containers.
