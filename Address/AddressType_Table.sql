--Creating AddressType Table to store type of address the user is adding

CREATE TABLE AddressType
(
AddressTypeId INT Primary Key,
Type VARCHAR(20)
);

INSERT INTO AddressType(AddressTypeId,Type)
VALUES(1,'Home'),
(2,'Work'),
(3,'Other');

