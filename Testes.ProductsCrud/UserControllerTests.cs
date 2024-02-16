﻿using Business.Interfaces.Services;
using Business.Models;
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
    }

    [Test]
    public void CreateUser_WithValidModel_RedirectsToIndex()
    {
        // Arrange
        var validUser = new User { /* Initialize a valid user object */ };
        _controller.ModelState.Clear(); // Clear any existing model errors

        // Act
        var result = _controller.CreateUser(validUser) as RedirectToActionResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("Index", result.ActionName);
        _userServiceMock.Verify(u => u.CreateUser(validUser), Times.Once);
    }

    [Test]
    public void CreateUser_WithInvalidModel_ReturnsViewWithModelError()
    {
        // Arrange
        var invalidUser = new User { /* Initialize an invalid user object */ };
        _controller.ModelState.AddModelError("UserName", "Username is required");

        // Act
        var result = _controller.CreateUser(invalidUser) as ViewResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(invalidUser, result.Model);
        _userServiceMock.Verify(u => u.CreateUser(It.IsAny<User>()), Times.Never);
    }

    [Test]
    public void Login_WithInvalidUser_ReturnsViewWithModelError()
    {
        // Arrange
        var invalidUser = new User { /* Initialize an invalid user object */ };
        _userServiceMock.Setup(u => u.ValidateUser(invalidUser)).Returns(false);

        // Act
        var result = _controller.Login(invalidUser) as ViewResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(invalidUser, result.Model);
        Assert.AreEqual("Invalid username or password", _controller.ModelState[""].Errors.First().ErrorMessage);
        _userServiceMock.Verify(u => u.ValidateUser(invalidUser), Times.Once);
    }
}