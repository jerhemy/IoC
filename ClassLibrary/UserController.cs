using SharedLibrary;

namespace ClassLibrary
{
    public class UserController: IUserController
    {
        private ILogger _logger;

        public UserController(ILogger logger)
        {
            _logger = logger;
        }
    }
}
