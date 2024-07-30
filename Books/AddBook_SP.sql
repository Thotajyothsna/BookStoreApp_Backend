--Stored Procedure for Adding Book Data to the Books Table in BookStore_DB

CREATE PROC AddBook
(
@BookName VARCHAR(100),
@AuthorName VARCHAR(50),
@Image VARCHAR(200),
@OriginalPrice INT,
@Quantity INT,
@DiscountRate SmallInt,
@Description VARCHAR(1000)
)
AS
BEGIN
	BEGIN TRY
        IF @BookName IS NULL OR @AuthorName IS NULL OR @OriginalPrice <= 0 OR @Quantity < 0
        BEGIN
            RAISERROR ('Invalid input data.', 16, 1);
        END

        INSERT INTO Books (BookName, AuthorName, Image, OriginalPrice, Quantity, DiscountRate, Rating, NoOfPeopleRated)
        VALUES (@BookName, @AuthorName, @Image, @OriginalPrice, @Quantity, @DiscountRate, 0, 0);

        -- Return 1 for success
        SELECT 1 AS Status;
    END TRY
    BEGIN CATCH
        -- Capture detailed error information
        SELECT 0 AS Status;
    END CATCH
END
