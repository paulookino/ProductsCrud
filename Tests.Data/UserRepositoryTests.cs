using Business.Interfaces.Repositories;
using Business.Models;
using Data.Repositories;
using NUnit.Framework;

namespace Tests.Data
{
    [TestFixture]
    public class UserRepositoryTests
    {
        [Test]
        public void CreateUser_ReturnsUserId()
        {
            // Arrange
            var mockRepository = new MockUserRepository();

            // Act
            var user = new User { };
            int userId = mockRepository.CreateUser(user);

            // Assert
            Assert.IsTrue(userId > 0);
        }

        [Test]
        public void ValidateUser_ReturnsTrueForValidUser()
        {
            // Arrange
            var mockRepository = new MockUserRepository();
            var existingUser = new User { };
            mockRepository.CreateUser(existingUser);

            // Act
            var userToValidate = new User { };
            bool result = mockRepository.ValidateUser(userToValidate);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void ValidateUser_ReturnsFalseForInvalidUser()
        {
            // Arrange
            var mockRepository = new MockUserRepository();
            var existingUser = new User { };
            mockRepository.CreateUser(existingUser);

            // Act
            var userToValidate = new User { Username = "user" };
            bool result = mockRepository.ValidateUser(userToValidate);

            // Assert
            Assert.IsFalse(result);
        }

    }

}
