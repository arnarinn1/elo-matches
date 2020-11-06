USE master;
GO  

if not exists (select 1 from sys.databases where name = 'Elo_Matches')
begin
  CREATE DATABASE Elo_Matches
  ON   
  ( NAME = Elo_Matches_Dat,  
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\Elo_Matches.mdf',  
    SIZE = 10,  
    MAXSIZE = 50,  
    FILEGROWTH = 5 )  
  LOG ON  
  ( NAME = Elo_Matches_Log,  
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\Elo_Matches.ldf',
    SIZE = 5MB,  
    MAXSIZE = 25MB,  
    FILEGROWTH = 5MB ) ;  
end
go