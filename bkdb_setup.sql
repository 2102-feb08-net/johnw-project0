-- set up schema
CREATE SCHEMA bkdb;
GO

-- set up tables
CREATE TABLE bkdb.Product (
	ID INT IDENTITY,
	Name NVARCHAR(255),
	Price NUMERIC(10, 2)
	CONSTRAINT PK_Product PRIMARY KEY CLUSTERED (ID)
);
GO
CREATE TABLE bkdb.Location (
	ID INT IDENTITY,
	Name NVARCHAR(255)
	CONSTRAINT PK_Location PRIMARY KEY CLUSTERED (ID)
);
GO
CREATE TABLE bkdb.Inventory (
	LocationID INT NOT NULL,
	ProductID INT NOT NULL,
	Amount INT NOT NULL
	CONSTRAINT CK_Inventory PRIMARY KEY (LocationID, ProductID)
);
GO
CREATE TABLE bkdb.Customer (
	ID INT IDENTITY,
	FirstName NVARCHAR(255),
	LastName NVARCHAR(255),
	DefaultLocationID INT
	CONSTRAINT PK_Customer PRIMARY KEY CLUSTERED (ID)
);
GO
CREATE TABLE bkdb.Orders (
	ID INT IDENTITY,
	CustomerID INT NOT NULL,
	LocationID INT NOT NULL,
	TotalPrice NUMERIC(10, 2),
	Time DATETIME
	CONSTRAINT PK_Order PRIMARY KEY CLUSTERED (ID)
);
GO
CREATE TABLE bkdb.OrderLine (
	OrderID INT NOT NULL,
	ProductID INT NOT NULL,
	Amount INT NOT NULL
	CONSTRAINT CK_OrderLine PRIMARY KEY (OrderID, ProductID)
);
GO


-- add foreign keys
ALTER TABLE bkdb.Inventory ADD CONSTRAINT FK_InventoryLocationID
	FOREIGN KEY (LocationID) REFERENCES bkdb.Location (ID) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
CREATE INDEX IFK_InventoryLocationID ON bkdb.Inventory (LocationID);
GO
ALTER TABLE bkdb.Inventory ADD CONSTRAINT FK_InventoryProductID
	FOREIGN KEY (ProductID) REFERENCES bkdb.Product (ID) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
CREATE INDEX IFK_InventoryProductID ON bkdb.Inventory (ProductID);
GO
ALTER TABLE bkdb.Orders ADD CONSTRAINT FK_OrdersCustomerID
	FOREIGN KEY (CustomerID) REFERENCES bkdb.Customer (ID) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
CREATE INDEX IFK_OrdersCustomerID ON bkdb.Orders (CustomerID);
GO
ALTER TABLE bkdb.Orders ADD CONSTRAINT FK_OrdersLocationID
	FOREIGN KEY (LocationID) REFERENCES bkdb.Location (ID) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
CREATE INDEX IFK_OrdersLocationID ON bkdb.Orders (LocationID);
GO

CREATE INDEX IFK_CustomerDefaultLocationID ON bkdb.Customer (DefaultLocationID);
GO
ALTER TABLE bkdb.OrderLine ADD CONSTRAINT FK_OrderLineOrderID
	FOREIGN KEY (OrderID) REFERENCES bkdb.Orders (ID) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
CREATE INDEX IFK_OrderLineOrderID ON bkdb.OrderLine (OrderID);
GO
ALTER TABLE bkdb.OrderLine ADD CONSTRAINT FK_OrderLineProductID
	FOREIGN KEY (ProductID) REFERENCES bkdb.Product (ID) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
CREATE INDEX IFK_OrderLineProductID ON bkdb.OrderLine (ProductID);
GO

-- populate tables
-- populate product table
INSERT INTO bkdb.Product (Name, Price) VALUES ('The Lord of the Rings: The Fellowship of the Rings', 12.00);
INSERT INTO bkdb.Product (Name, Price) VALUES ('The Lord of the Rings: The Two Towers', 12.00);
INSERT INTO bkdb.Product (Name, Price) VALUES ('The Lord of the Rings: The Return King', 12.00);
INSERT INTO bkdb.Product (Name, Price) VALUES ('The Hobbit', 12.00);
INSERT INTO bkdb.Product (Name, Price) VALUES ('Harry Potter and the Sorcerer''s Stone', 10.99);
INSERT INTO bkdb.Product (Name, Price) VALUES ('Harry Potter and the Chamber of Secrets', 10.99);
INSERT INTO bkdb.Product (Name, Price) VALUES ('Harry Potter and the Prizoner of Azkaban', 10.99);
INSERT INTO bkdb.Product (Name, Price) VALUES ('Harry Potter and the Goblet of Fire', 12.99);
INSERT INTO bkdb.Product (Name, Price) VALUES ('Harry Potter and the Order of the Phoenix', 12.99);
INSERT INTO bkdb.Product (Name, Price) VALUES ('Harry Potter and the Half-Blood Prince', 12.99);
INSERT INTO bkdb.Product (Name, Price) VALUES ('Harry Potter and the Deathly Hallows', 12.99);
INSERT INTO bkdb.Product (Name, Price) VALUES ('Eragon', 16.95);
INSERT INTO bkdb.Product (Name, Price) VALUES ('Eldest', 16.95);
INSERT INTO bkdb.Product (Name, Price) VALUES ('Brisingr', 16.95);
INSERT INTO bkdb.Product (Name, Price) VALUES ('Inheritance', 16.95);

-- populate location table
INSERT INTO bkdb.Location (Name) VALUES ('Salt Lake City Store');
INSERT INTO bkdb.Location (Name) VALUES ('Provo Store');
INSERT INTO bkdb.Location (Name) VALUES ('St. George Store');
INSERT INTO bkdb.Location (Name) VALUES ('Las Vegas Store');

-- populate inventory table
INSERT INTO bkdb.Inventory (LocationID, ProductID, Amount) VALUES (1, 1, 25);
INSERT INTO bkdb.Inventory (LocationID, ProductID, Amount) VALUES (1, 2, 18);
INSERT INTO bkdb.Inventory (LocationID, ProductID, Amount) VALUES (1, 3, 11);
INSERT INTO bkdb.Inventory (LocationID, ProductID, Amount) VALUES (1, 4, 6);
INSERT INTO bkdb.Inventory (LocationID, ProductID, Amount) VALUES (1, 7, 30);
INSERT INTO bkdb.Inventory (LocationID, ProductID, Amount) VALUES (1, 8, 21);
INSERT INTO bkdb.Inventory (LocationID, ProductID, Amount) VALUES (1, 10, 4);
INSERT INTO bkdb.Inventory (LocationID, ProductID, Amount) VALUES (1, 11, 16);
INSERT INTO bkdb.Inventory (LocationID, ProductID, Amount) VALUES (1, 13, 3);
INSERT INTO bkdb.Inventory (LocationID, ProductID, Amount) VALUES (1, 14, 5);

INSERT INTO bkdb.Inventory (LocationID, ProductID, Amount) VALUES (2, 4, 3);
INSERT INTO bkdb.Inventory (LocationID, ProductID, Amount) VALUES (2, 5, 7);
INSERT INTO bkdb.Inventory (LocationID, ProductID, Amount) VALUES (2, 6, 12);
INSERT INTO bkdb.Inventory (LocationID, ProductID, Amount) VALUES (2, 7, 5);
INSERT INTO bkdb.Inventory (LocationID, ProductID, Amount) VALUES (2, 8, 14);
INSERT INTO bkdb.Inventory (LocationID, ProductID, Amount) VALUES (2, 9, 19);
INSERT INTO bkdb.Inventory (LocationID, ProductID, Amount) VALUES (2, 10, 8);
INSERT INTO bkdb.Inventory (LocationID, ProductID, Amount) VALUES (2, 11, 11);

INSERT INTO bkdb.Inventory (LocationID, ProductID, Amount) VALUES (3, 12, 10);
INSERT INTO bkdb.Inventory (LocationID, ProductID, Amount) VALUES (3, 13, 8);
INSERT INTO bkdb.Inventory (LocationID, ProductID, Amount) VALUES (3, 14, 13);
INSERT INTO bkdb.Inventory (LocationID, ProductID, Amount) VALUES (3, 15, 2);
INSERT INTO bkdb.Inventory (LocationID, ProductID, Amount) VALUES (3, 1, 6);
INSERT INTO bkdb.Inventory (LocationID, ProductID, Amount) VALUES (3, 2, 19);
INSERT INTO bkdb.Inventory (LocationID, ProductID, Amount) VALUES (3, 3, 14);
INSERT INTO bkdb.Inventory (LocationID, ProductID, Amount) VALUES (3, 4, 15);

INSERT INTO bkdb.Inventory (LocationID, ProductID, Amount) VALUES (4, 1, 11);
INSERT INTO bkdb.Inventory (LocationID, ProductID, Amount) VALUES (4, 2, 11);
INSERT INTO bkdb.Inventory (LocationID, ProductID, Amount) VALUES (4, 3, 11);
INSERT INTO bkdb.Inventory (LocationID, ProductID, Amount) VALUES (4, 4, 11);
INSERT INTO bkdb.Inventory (LocationID, ProductID, Amount) VALUES (4, 5, 7);
INSERT INTO bkdb.Inventory (LocationID, ProductID, Amount) VALUES (4, 6, 7);
INSERT INTO bkdb.Inventory (LocationID, ProductID, Amount) VALUES (4, 7, 7);
INSERT INTO bkdb.Inventory (LocationID, ProductID, Amount) VALUES (4, 8, 7);
INSERT INTO bkdb.Inventory (LocationID, ProductID, Amount) VALUES (4, 9, 7);
INSERT INTO bkdb.Inventory (LocationID, ProductID, Amount) VALUES (4, 10, 7);
INSERT INTO bkdb.Inventory (LocationID, ProductID, Amount) VALUES (4, 11, 7);
INSERT INTO bkdb.Inventory (LocationID, ProductID, Amount) VALUES (4, 12, 15);
INSERT INTO bkdb.Inventory (LocationID, ProductID, Amount) VALUES (4, 13, 15);
INSERT INTO bkdb.Inventory (LocationID, ProductID, Amount) VALUES (4, 14, 15);
INSERT INTO bkdb.Inventory (LocationID, ProductID, Amount) VALUES (4, 15, 15);

-- populate customer table
INSERT INTO bkdb.Customer (FirstName, LastName, DefaultLocationID) VALUES ('John', 'Werner', 2);
INSERT INTO bkdb.Customer (FirstName, LastName, DefaultLocationID) VALUES ('Nick', 'Escalona', 4);
INSERT INTO bkdb.Customer (FirstName, LastName, DefaultLocationID) VALUES ('Abe', 'Froman', 3);
INSERT INTO bkdb.Customer (FirstName, LastName, DefaultLocationID) VALUES ('Derek', 'Jones', 1);

-- populate orders table
INSERT INTO bkdb.Orders (CustomerID, LocationID, TotalPrice, Time) VALUES (1, 2, 48.00, '20210220 02:11:25 PM');
INSERT INTO bkdb.Orders (CustomerID, LocationID, TotalPrice, Time) VALUES (2, 4, 21.98, '20201020 09:45:16 AM');
INSERT INTO bkdb.Orders (CustomerID, LocationID, TotalPrice, Time) VALUES (3, 3, 84.93, '20210106 12:22:48 PM');
INSERT INTO bkdb.Orders (CustomerID, LocationID, TotalPrice, Time) VALUES (4, 1, 67.80, '20200611 10:33:51 AM');

-- populate orderline table
INSERT INTO bkdb.OrderLine (OrderID, ProductID, Amount) VALUES (1, 1, 1);
INSERT INTO bkdb.OrderLine (OrderID, ProductID, Amount) VALUES (1, 2, 1);
INSERT INTO bkdb.OrderLine (OrderID, ProductID, Amount) VALUES (1, 3, 1);
INSERT INTO bkdb.OrderLine (OrderID, ProductID, Amount) VALUES (1, 4, 1);
INSERT INTO bkdb.OrderLine (OrderID, ProductID, Amount) VALUES (2, 5, 1);
INSERT INTO bkdb.OrderLine (OrderID, ProductID, Amount) VALUES (2, 6, 1);
INSERT INTO bkdb.OrderLine (OrderID, ProductID, Amount) VALUES (3, 5, 1);
INSERT INTO bkdb.OrderLine (OrderID, ProductID, Amount) VALUES (3, 6, 1);
INSERT INTO bkdb.OrderLine (OrderID, ProductID, Amount) VALUES (3, 7, 1);
INSERT INTO bkdb.OrderLine (OrderID, ProductID, Amount) VALUES (3, 8, 1);
INSERT INTO bkdb.OrderLine (OrderID, ProductID, Amount) VALUES (3, 9, 1);
INSERT INTO bkdb.OrderLine (OrderID, ProductID, Amount) VALUES (3, 10, 1);
INSERT INTO bkdb.OrderLine (OrderID, ProductID, Amount) VALUES (3, 11, 1);
INSERT INTO bkdb.OrderLine (OrderID, ProductID, Amount) VALUES (4, 13, 4);
GO