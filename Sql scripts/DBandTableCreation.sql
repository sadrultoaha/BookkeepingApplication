IF DB_ID('BookkeepingSqlDB') IS NULL
	Create database BookkeepingSqlDB
Go

USE BookkeepingSqlDB;
Go

IF OBJECT_ID(N'dbo.PredefinedRecords', N'U') IS NOT NULL  
   DROP TABLE [BookkeepingSqlDB].[dbo].[PredefinedRecords];  
GO

IF OBJECT_ID(N'dbo.ReconciliationRecords', N'U') IS NOT NULL  
   DROP TABLE [dbo].[ReconciliationRecords];  
GO

IF OBJECT_ID(N'dbo.RecordTypes', N'U') IS NOT NULL  
   DROP TABLE [BookkeepingSqlDB].[dbo].[RecordTypes];  
GO

CREATE Table RecordTypes(
Id int NOT NULL IDENTITY(1,1) PRIMARY KEY,
ActionName nvarchar(20) NOT NULL,
TypeName nvarchar(50) NOT NULL,
);
GO

CREATE Table PredefinedRecords(
Id int NOT NULL IDENTITY(1,1) PRIMARY KEY,
TypeId int NOT NULL FOREIGN KEY REFERENCES RecordTypes(Id),
Date datetime NOT NULL,
Amount decimal(18,2) NOT NULL,
);
GO

CREATE Table ReconciliationRecords(
Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
TypeId int NOT NULL FOREIGN KEY REFERENCES RecordTypes(Id),
Year int NOT NULL,
Jan decimal(18, 2) NOT NULL DEFAULT 0,
Feb decimal(18, 2) NOT NULL DEFAULT 0,
Mar decimal(18, 2) NOT NULL DEFAULT 0,
Apr decimal(18, 2) NOT NULL DEFAULT 0,
May decimal(18, 2) NOT NULL DEFAULT 0,
Jun decimal(18, 2) NOT NULL DEFAULT 0,
Jul decimal(18, 2) NOT NULL DEFAULT 0,
Aug decimal(18, 2) NOT NULL DEFAULT 0,
Sep decimal(18, 2) NOT NULL DEFAULT 0,
Oct decimal(18, 2) NOT NULL DEFAULT 0,
Nov decimal(18, 2) NOT NULL DEFAULT 0,
Dec decimal(18, 2) NOT NULL DEFAULT 0,
CONSTRAINT UC_TypeId_Year UNIQUE (TypeId,Year)
);
GO
