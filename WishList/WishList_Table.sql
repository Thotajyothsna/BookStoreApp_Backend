--Creating WishList Table in BookStore_DB

CREATE TABLE WishList
(
WishListId BIGINT IDENTITY(1,1),
BookId INT,
UserId INT,
CONSTRAINT FK_WishListBooks FOREIGN KEY (BookId) REFERENCES Books(BookId),
CONSTRAINT FK_WishListSignUp FOREIGN KEY (UserId) REFERENCES SignUp(UserId)
)