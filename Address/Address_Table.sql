--Creating Address Table In BookStore_DB to store Addresses Added By User

CREATE TABLE Address
(
AddressId INT IDENTITY(1,1) PRIMARY KEY,
Address VARCHAR(200) NOT NULL,
City VARCHAR(50) NOT NULL,
State VARCHAR(50) NOT NULL,
UserId INT,
AddressTypeId INT,
CONSTRAINT FK_UserId FOREIGN KEY(UserId) REFERENCES SignUp(UserId),
CONSTRAINT FK_TypeId FOREIGN KEY(AddressTypeId) REFERENCES AddressType(AddressTypeId)
);