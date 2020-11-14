USE master;
GO  

if not exists (select 1 from sys.databases where name = 'Metrics')
begin
  CREATE DATABASE Metrics
  ON   
  ( NAME = Metrics_Dat,  
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\Metrics.mdf',  
    SIZE = 10,  
    MAXSIZE = 50,  
    FILEGROWTH = 5 )  
  LOG ON  
  ( NAME = Metrics_Log,  
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\Metrics.ldf',
    SIZE = 5MB,  
    MAXSIZE = 25MB,  
    FILEGROWTH = 5MB ) ;  
end
go