using Business.Models;

namespace Business.Interfaces.Repositories
{
    public interface IUserRepository
    {
        int CreateUser(User user);
        bool ValidateUser(User user);

    }
}
