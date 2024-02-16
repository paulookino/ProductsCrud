using Business.Interfaces.Repositories;
using Business.Interfaces.Services;
using Business.Models;

namespace Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public int CreateUser(User user)
        {
            return _userRepository.CreateUser(user);
        }

        public bool ValidateUser(User user)
        {
            return _userRepository.ValidateUser(user);
        }

    }
}
