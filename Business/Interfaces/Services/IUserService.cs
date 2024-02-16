using Business.Models;

namespace Business.Interfaces.Services
{
    public interface IUserService
    {
        int CreateUser(User user);
        bool ValidateUser(User user);

    }
}
