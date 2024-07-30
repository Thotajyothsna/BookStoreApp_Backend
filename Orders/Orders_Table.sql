--Creating Orders table of storing Orders Placed by User

CREATE TABLE ORDERS (
    OrderId INT PRIMARY KEY IDENTITY(1,1),
    CartId BIGINT NULL,
	UserId INT NOT NULL,
	AddressId INT NOT NULL,
    Quantity INT,
    OriginalPrice INT,
    DiscountRate SMALLINT,
    DiscountPrice SMALLINT,
    TotalPrice DECIMAL(10, 2),
	OrderPlacedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (CartId) REFERENCES CART(CartId) ON DELETE SET NULL,
	FOREIGN KEY (UserId) REFERENCES SignUp(UserId) ON DELETE CASCADE,
	 FOREIGN KEY (AddressId) REFERENCES Address(AddressId)
);

