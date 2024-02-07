----DECLARE @FirstName nvarchar(50) SET @FirstName = 'TestUSER'
----DECLARE @Lastname nvarchar(50) SET @Lastname = 'Testsson'
----DECLARE @Email nvarchar(50) SET @Email = 'test5@email.com'
----DECLARE @PhoneNumber varchar(10) SET @PhoneNumber = '12345678'
----DECLARE @AdressId int SET @AdressId = 1
----DECLARE @RoleId int SET @RoleId = 2


----IF NOT EXISTS(SELECT 1 FROM Customers WHERE Email = @Email) 
----INSERT INTO Customers OUTPUT INSERTED.ID Values (@FirstName, @LastName, @Email, @PhoneNumber, @AdressId, @RoleId)


----DECLARE @Id int SET @Id = 3
----DECLARE @RoleName nvarchar(max) SET @RoleName = 'tester3'
----INSERT INTO Roles VALUES(@Id, @RoleName)


----DECLARE @StreetName nvarchar(50) SET @StreetName = 'testvägen 1'
----DECLARE @PostalCode varchar(6) SET @PostalCode = '123456'
----DECLARE @City nvarchar(50) SET @City = 'Stockholm'

----IF NOT EXISTS(SELECT 1 FROM Adresses WHERE StreetName = @StreetName AND PostalCode = @PostalCode AND City = @City) 
----INSERT INTO Adresses OUTPUT INSERTED.ID Values (@StreetName, @City, @PostalCode) 
----ELSE SELECT Id FROM Adresses WHERE StreetName = @StreetName AND PostalCode = @PostalCode AND City = @City


--DECLARE @Id int SET @Id = 0 DECLARE @RoleName nvarchar(max) SET @RoleName = 'TestUser' 
--IF NOT EXISTS (SELECT 1 FROM Roles WHERE Id = @Id) INSERT INTO Roles VALUES(@Id, @RoleName)
--DECLARE @Id int SET @Id = 1 DECLARE @RoleName nvarchar(max) SET @RoleName = 'User' 
--IF NOT EXISTS (SELECT 1 FROM Roles WHERE Id = @Id) INSERT INTO Roles VALUES(@Id, @RoleName)
--DECLARE @Id int SET @Id = 2 DECLARE @RoleName nvarchar(max) SET @RoleName = 'Admin' 
--IF NOT EXISTS (SELECT 1 FROM Roles WHERE Id = @Id) INSERT INTO Roles VALUES(@Id, @RoleName)

----DECLARE @Id int SET @Id = 5555554
----DECLARE @CategoryName NVarchar(max) SET @CategoryName = 'testcategory'
----INSERT INTO Categories Values(@Id, @CategoryName)

--Select * From Products

--Select * From Categories

USE [C:\USERS\R\DOWNLOADS\DB\CONSOLEAPPD\CONSOLEAPPD\DATA\DATABASE.MDF];  
GO  
SELECT   
    f.name AS foreign_key_name  
   ,OBJECT_NAME(f.parent_object_id) AS table_name  
   ,COL_NAME(fc.parent_object_id, fc.parent_column_id) AS constraint_column_name  
   ,OBJECT_NAME (f.referenced_object_id) AS referenced_object  
   ,COL_NAME(fc.referenced_object_id, fc.referenced_column_id) AS referenced_column_name  
   ,f.is_disabled, f.is_not_trusted
   ,f.delete_referential_action_desc  
   ,f.update_referential_action_desc  
FROM sys.foreign_keys AS f  
INNER JOIN sys.foreign_key_columns AS fc   
   ON f.object_id = fc.constraint_object_id

