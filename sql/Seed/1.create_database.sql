USE master;
GO  

if not exists (select 1 from sys.databases where name = 'Foosball')
begin
  CREATE DATABASE Foosball
  ON   
  ( NAME = Foosball_Dat,  
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\Foosball.mdf',  
    SIZE = 10,  
    MAXSIZE = 50,  
    FILEGROWTH = 5 )  
  LOG ON  
  ( NAME = Foosball_Log,  
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\Foosball.ldf',
    SIZE = 5MB,  
    MAXSIZE = 25MB,  
    FILEGROWTH = 5MB ) ;  
end
go