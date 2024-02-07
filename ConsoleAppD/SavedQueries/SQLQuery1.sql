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


   IF NOT EXISTS 
   (SELECT 1 FROM Adresses WHERE StreetName = @StreetName AND City = @City AND PostalCode = @PostalCode) 
   INSERT INTO Adresses 
   OUTPUT INSERTED.Id 
   Values (@StreetName, @City, @PostalCode) 
   ELSE 
   SELECT Id 
   FROM Adresses 
   WHERE StreetName = @StreetName AND PostalCode = @PostalCode AND City = @City