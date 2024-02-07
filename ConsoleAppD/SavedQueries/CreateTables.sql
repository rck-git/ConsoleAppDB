drop table Products
drop table Categories

drop table Customers
drop table Roles
drop table Adresses


--CREATE TABLE Categories 
--(
--Id int not null identity primary key,
--CategoryName nvarchar(100) unique
--)
--CREATE TABLE Products
--( 
--Id int not null identity primary key,
--Title nvarchar(max) not null,
--Price money not null,
--CategoryId int not null references Categories(Id)
--)

--CREATE TABLE Roles 
--(
--Id int not null primary key,
--RoleName nvarchar(100) not null unique 
--)

--CREATE TABLE Adresses
--(
--Id int not null identity primary key,
--StreetName nvarchar(50) not null,
--City nvarchar(50) not null,
--PostalCode varchar(6) not null
--)


--CREATE TABLE Customers
--(
--Id int not null identity primary key,
--FirstName nvarchar(50) not null,
--LastName nvarchar(50) not null,
--Email nvarchar(100) not null unique,
--PhoneNumber varchar(10) null,
--AdressId int not null references Adresses(Id),
--RoleId int not null references Roles(Id)
--)

--DECLARE @Id int SET @Id = 0 
--DECLARE @RoleName nvarchar(max) SET @RoleName = 'TestUser' 
--IF NOT EXISTS (SELECT 1 FROM Roles WHERE Id = @Id) 
--INSERT INTO Roles VALUES(@Id, @RoleName)

--DECLARE @Id1 int SET @Id1 = 1 
--DECLARE @RoleName1 nvarchar(max) SET @RoleName1 = 'User' 
--IF NOT EXISTS (SELECT 1 FROM Roles WHERE Id = @Id1) 
--INSERT INTO Roles VALUES(@Id1, @RoleName1)

--DECLARE @Id2 int SET  @Id2 = 2 
--DECLARE @RoleName2 nvarchar(max) SET @RoleName2 = 'Admin' 
--IF NOT EXISTS (SELECT 1 FROM Roles WHERE Id = @Id2) 
--INSERT INTO Roles VALUES(@Id2, @RoleName2)

