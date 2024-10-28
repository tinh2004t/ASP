use quanlybanhang
-- Tạo bảng User
CREATE TABLE [User] (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(255) NOT NULL UNIQUE,
    [Password] NVARCHAR(255) NOT NULL,
    FullName NVARCHAR(255),
    Email NVARCHAR(255) UNIQUE,
    Phone NVARCHAR(15),
    Address NVARCHAR(255),
    Role NVARCHAR(50) DEFAULT 'customer' CHECK (Role IN ('customer', 'admin')),
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- Tạo bảng Warehouse
CREATE TABLE Warehouse (
    WarehouseID INT PRIMARY KEY IDENTITY(1,1),
    WarehouseName NVARCHAR(255),
    Location NVARCHAR(255),
    Capacity INT
);

-- Tạo bảng Product
CREATE TABLE Product (
    ProductID INT PRIMARY KEY IDENTITY(1,1),
    ProductName NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX),
    Category NVARCHAR(255),
    Price DECIMAL(10, 2) NOT NULL,
    StockQuantity INT,
    ImageURL NVARCHAR(255),
    WarehouseID INT,
    FOREIGN KEY (WarehouseID) REFERENCES Warehouse(WarehouseID)
);

-- Tạo bảng Cart
CREATE TABLE Cart (
    CartID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT,
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserID) REFERENCES [User](UserID)
);

-- Tạo bảng CartItem
CREATE TABLE CartItem (
    CartItemID INT PRIMARY KEY IDENTITY(1,1),
    CartID INT,
    ProductID INT,
    Quantity INT NOT NULL,
    FOREIGN KEY (CartID) REFERENCES Cart(CartID),
    FOREIGN KEY (ProductID) REFERENCES Product(ProductID)
);

-- Tạo bảng Order
CREATE TABLE [Order] (
    OrderID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT,
    OrderDate DATETIME DEFAULT GETDATE(),
    TotalAmount DECIMAL(10, 2),
    ShippingFee DECIMAL(10, 2),
    Status NVARCHAR(50) DEFAULT 'Pending' CHECK (Status IN ('Pending', 'Processing', 'Shipped', 'Delivered', 'Cancelled')),
    ShippingAddress NVARCHAR(255),
    PaymentStatus NVARCHAR(50) DEFAULT 'Unpaid' CHECK (PaymentStatus IN ('Unpaid', 'Paid')),
    PaymentMethod NVARCHAR(50) CHECK (PaymentMethod IN ('Credit Card', 'Cash on Delivery', 'Paypal')),
    FOREIGN KEY (UserID) REFERENCES [User](UserID)
);

-- Tạo bảng OrderDetail
CREATE TABLE OrderDetail (
    OrderDetailID INT PRIMARY KEY IDENTITY(1,1),
    OrderID INT,
    ProductID INT,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(10, 2),
    TotalPrice AS (Quantity * UnitPrice),
    FOREIGN KEY (OrderID) REFERENCES [Order](OrderID),
    FOREIGN KEY (ProductID) REFERENCES Product(ProductID)
);

-- Tạo bảng Payment
CREATE TABLE Payment (
    PaymentID INT PRIMARY KEY IDENTITY(1,1),
    OrderID INT,
    PaymentDate DATETIME,
    PaymentAmount DECIMAL(10, 2),
    PaymentMethod NVARCHAR(50) CHECK (PaymentMethod IN ('Credit Card', 'Cash on Delivery', 'Paypal')),
    FOREIGN KEY (OrderID) REFERENCES [Order](OrderID)
);

INSERT INTO [User] (Username, [Password], FullName, Email, Phone, Address, Role, CreatedAt) VALUES 
('user1', 'password123', 'Nguyen Van A', 'user1@example.com', '0901234567', 'Hanoi', 'customer', GETDATE()),
('user2', 'password123', 'Tran Thi B', 'user2@example.com', '0902234567', 'Danang', 'customer', GETDATE()),
('admin1', 'adminpassword', 'Le Van C', 'admin1@example.com', '0903234567', 'Saigon', 'admin', GETDATE());

INSERT INTO Warehouse (WarehouseName, Location, Capacity) VALUES 
(N'Kho Hà Nội', 'Hanoi', 1000),
(N'Kho Đà Nẵng', 'Danang', 800),
(N'Kho Sài Gòn', 'Saigon', 1200);

INSERT INTO Product (ProductName, Description, Category, Price, StockQuantity, ImageURL, WarehouseID) VALUES 
('Laptop Acer Aspire', N'Laptop cho sinh viên', 'Electronics', 15000.00, 50, 'https://example.com/image1.jpg', 1),
('Điện thoại iPhone 14', N'Điện thoại cao cấp', 'Electronics', 20000.00, 30, 'https://example.com/image2.jpg', 2),
('Máy ảnh Canon', N'Máy ảnh chụp hình chất lượng cao', 'Electronics', 12000.00, 20, 'https://example.com/image3.jpg', 3);

INSERT INTO Cart (UserID, CreatedAt) VALUES 
(1, GETDATE()),
(2, GETDATE()),
(1, GETDATE());

INSERT INTO CartItem (CartID, ProductID, Quantity) VALUES 
(1, 1, 2),
(1, 2, 1),
(2, 3, 3);

INSERT INTO [Order] (UserID, OrderDate, TotalAmount, ShippingFee, Status, ShippingAddress, PaymentStatus, PaymentMethod) VALUES 
(1, GETDATE(), 30000.00, 1000.00, 'Pending', 'Hanoi', 'Unpaid', 'Credit Card'),
(2, GETDATE(), 20000.00, 500.00, 'Processing', 'Danang', 'Paid', 'Cash on Delivery'),
(1, GETDATE(), 25000.00, 750.00, 'Shipped', 'Saigon', 'Unpaid', 'Paypal');

INSERT INTO OrderDetail (OrderID, ProductID, Quantity, UnitPrice) VALUES 
(1, 1, 1, 15000.00),
(1, 2, 1, 20000.00),
(2, 3, 2, 12000.00);

INSERT INTO Payment (OrderID, PaymentDate, PaymentAmount, PaymentMethod) VALUES 
(1, GETDATE(), 31000.00, 'Credit Card'),
(2, GETDATE(), 20500.00, 'Cash on Delivery'),
(3, GETDATE(), 25750.00, 'Paypal');

