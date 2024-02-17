using Business.Interfaces.Repositories;
using Business.Models;
using System.Collections.Generic;


namespace Tests.Data
{
    public class MockUserRepository : IUserRepository
    {
        private List<User> users;

        public MockUserRepository()
        {
            users = new List<User>();
        }

        public int CreateUser(User user)
        {
            user.Id = users.Count + 1;
            users.Add(user);
            return user.Id;
        }

        public bool ValidateUser(User user)
        {
            return users.Any(u => u.Username == user.Username && u.Password == user.Password);
        }

        public IEnumerable<User> GetUsers()
        {
            return users;
        }
    }

}
