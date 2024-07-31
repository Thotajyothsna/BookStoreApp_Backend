--Creating Address Table for BookStoreApp

CREATE TABLE Address
(
AddressId INT IDENTITY(1,1) PRIMARY KEY,
Address VARCHAR(200) NOT NULL,
City VARCHAR(50) NOT NULL,
State VARCHAR(50) NOT NULL,
UserId int,
AddressTypeId int,
Constraint FK_TypeId FOREIGN KEY (AddressTypeId) REFERENCES AddressType(AddressTypeId),
Constraint FK_UserId FOREIGN KEY (UserId) REFERENCES SignUp(UserId)
)