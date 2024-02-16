using System;
using Business.Interfaces.Repositories;
using Business.Models;
using Business.Services;
using Moq;
using NUnit.Framework;

namespace Tests.Business
{
    [TestFixture]
    public class UserServiceTests
    {
        [Test]
        public void CreateUser_Should_Call_UserRepository_CreateUser()
        {
            // Arrange
            var mockUserRepository = new Mock<IUserRepository>();
            var userService = new UserService(mockUserRepository.Object);
            var user = new User { Username = "testuser", Password = "testpassword" };

            // Act
            userService.CreateUser(user);

            // Assert
            mockUserRepository.Verify(repo => repo.CreateUser(user), Times.Once);
        }

        [Test]
        public void ValidateUser_Should_Call_UserRepository_ValidateUser()
        {
            // Arrange
            var mockUserRepository = new Mock<IUserRepository>();
            var userService = new UserService(mockUserRepository.Object);
            var user = new User { Username = "testuser", Password = "testpassword" };

            // Act
            userService.ValidateUser(user);

            // Assert
            mockUserRepository.Verify(repo => repo.ValidateUser(user), Times.Once);
        }
    }
}
