USE master;
GO

CREATE DATABASE ProductsDB;
GO


USE ProductsDB;
GO

CREATE TABLE Products
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(255) NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
    Stock INT NOT NULL,
    Active BIT NOT NULL,
    CreatedAt DATETIME NOT NULL,
    UpdatedAt DATETIME NULL,
    DeletedAt DATETIME NULL
);

CREATE TABLE Users
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL,
    Password NVARCHAR(50) NOT NULL
);

--Server: localhost,1433
--Authentication: SQL Server Authentication
--Login: sa
--Password: YourPassword123