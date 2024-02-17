using Business.Interfaces.Services;
using Business.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ProductsCrud.Controllers;

namespace Testes.ProductsCrud;

[TestFixture]
public class UserControllerTests
{
    private Mock<IUserService> _userServiceMock;
    private UserController _controller;

    [SetUp]
    public void Setup()
    {
        _userServiceMock = new Mock<IUserService>();
        _controller = new UserController(_userServiceMock.Object);
        _controller.ControllerContext = new ControllerContext() { HttpContext = new DefaultHttpContext() };
    }

    [Test]
    public void CreateUser_ReturnsView()
    {
        // Act
        var result = _controller.CreateUser();

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
    }

    [Test]
    public void CreateUser_InvalidModel_ReturnsView()
    {
        // Arrange
        var invalidUser = new User { };
        _controller.ModelState.AddModelError("Username", "Username is required");

        // Act
        var result = _controller.CreateUser(invalidUser);

        // Assert

        Assert.IsInstanceOf<ViewResult>(result);
        Assert.IsInstanceOf<User>(((ViewResult)result).Model);
    }

    [Test]
    public void Login_ReturnsView()
    {
        // Act
        var result = _controller.Login();

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
    }

    [Test]
    public void Login_InvalidUser_ReturnsViewWithModelError()
    {
        // Arrange
        var invalidUser = new User { Username  = "username" };
        _userServiceMock.Setup(service => service.ValidateUser(invalidUser)).Returns(false);

        // Act
        var result = _controller.Login(invalidUser);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        Assert.IsInstanceOf<User>(((ViewResult)result).Model);
        Assert.IsFalse(_controller.ModelState.IsValid);
        Assert.IsTrue(_controller.ModelState.ContainsKey(string.Empty));
    }
}
