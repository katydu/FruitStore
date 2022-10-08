--Drop Database
IF DB_ID('FruitStore') IS NOT NULL DROP DATABASE FruitStore;

-- If database could not be created due to open connections, abort
IF @@ERROR = 3702 
   RAISERROR('Database cannot be dropped because there are still open connections.', 127, 127) WITH NOWAIT, LOG;

-- Create database
CREATE DATABASE FruitStore;
GO

USE FruitStore;
GO

CREATE TABLE Members
(
	[MemberID]      INT IDENTITY(1,1)   NOT NULL,
	[MemberName]    NVARCHAR(20)		NOT NULL,
	[MemberEmail]   NVARCHAR(50)		NOT NULL,
	[MemberAddress] NVARCHAR(70)		NOT NULL,
	[MemberPhone]   NVARCHAR(24)		NOT NULL,
    PRIMARY KEY(MemberID),
);
GO

CREATE TABLE Orders
(
  [OrderID]         INT  IDENTITY(1,1)      NOT NULL,
  [MemberID]        INT						NOT NULL,
  [OrderTime]       DATETIME				NULL,
  [OrderFee]        INT						NULL,
  [OrderTotalPrice] INT						NULL,
  PRIMARY KEY(OrderID),
  FOREIGN KEY(MemberID) REFERENCES Members(MemberID),
);


CREATE TABLE Products
(
  [ProductID]         INT    IDENTITY(1,1)      NOT NULL,
  [ProductName]       NVARCHAR(20) NOT NULL,
  [ProductUnitPrice]  INT		   NOT NULL,
  [ProductImg]        NVARCHAR(60) NULL,
  PRIMARY KEY(ProductID),
);

CREATE TABLE OrderDetails
(
  [OrderID]      INT  NOT NULL,
  [ProductID]    INT  NOT NULL,
  [Quantity]     INT  NULL,
  PRIMARY KEY(OrderID,ProductID),
  FOREIGN KEY(OrderID) REFERENCES Orders(OrderID),
  FOREIGN KEY(ProductID) REFERENCES Products(ProductID),
);

--Insert Members data
INSERT INTO Members([MemberName], [MemberEmail], [MemberAddress], [MemberPhone])
  VALUES(N'張三', N'123@gmail.com', N'桃園市中壢區環東路56號', N'0911111221');
INSERT INTO Members([MemberName], [MemberEmail], [MemberAddress], [MemberPhone])
  VALUES(N'李四', N'456@gmail.com', N'桃園市桃園區區環東路56號', N'0915611111');
INSERT INTO Members([MemberName], [MemberEmail], [MemberAddress], [MemberPhone])
  VALUES(N'陳武', N'789@gmail.com', N'桃園市龍潭區環東路56號', N'0911111891');

--Insert Orders data
INSERT INTO Orders([MemberID], [OrderTime], [OrderFee], [OrderTotalPrice])
  VALUES(N'1', N'2019/12/27 00:00:00', N'250', N'1000');
INSERT INTO Orders([MemberID], [OrderTime], [OrderFee], [OrderTotalPrice])
  VALUES(N'3', N'2020/10/01 00:00:00', N'250', N'600');

--Insert Products data
INSERT INTO Products([ProductName], [ProductUnitPrice], [ProductImg])
  VALUES(N'香蕉', N'100', NULL);
INSERT INTO Products([ProductName], [ProductUnitPrice], [ProductImg])
  VALUES(N'蘋果', N'250', NULL);

--Insert OrderDetails data
INSERT INTO OrderDetails([OrderID], [ProductID], [Quantity])
  VALUES(N'1', N'2', N'4');
INSERT INTO OrderDetails([OrderID], [ProductID], [Quantity])
  VALUES(N'2', N'1', N'6');