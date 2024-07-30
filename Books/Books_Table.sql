--Creating Books Table in BookStore_DB having all details of a book

CREATE TABLE Books
(
BookId INT IDENTITY(1,1) PRIMARY KEY,
BookName VARCHAR(100),
AuthorName VARCHAR(50),
Image VARCHAR(200),
OriginalPrice INT,
Quantity INT,
DiscountRate SmallInt Check (DiscountRate < 100),
Rating SmallInt,
NoOfPeopleRated INT,
DiscountPrice as (OriginalPrice-(OriginalPrice*DiscountRate/100))
);
