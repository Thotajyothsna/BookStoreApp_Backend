--Creating Table SignUp for BookStore App

CREATE TABLE SignUp
(
UserId INT IDENTITY(1,1) PRIMARY KEY,
FullName VARCHAR(50),
EmailId VARCHAR(100) UNIQUE,
Password VARCHAR(50),
MobileNumber VARCHAR(18)
);

