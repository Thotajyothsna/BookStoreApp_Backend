--Updating the Book Data

CREATE PROCEDURE UpdateBook
(
    @BookId INT,
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
        -- Check if the BookId exists before attempting to update
        IF NOT EXISTS (SELECT 1 FROM Books WHERE BookId = @BookId)
        BEGIN
            THROW 50001, 'BookId not found in Books table.', 1;
        END

        -- Update the book with the given BookId
        UPDATE Books
        SET 
            BookName = @BookName,
            AuthorName = @AuthorName,
            Image = @Image,
            OriginalPrice = @OriginalPrice,
            Quantity = @Quantity,
            DiscountRate = @DiscountRate,
            Rating = 0,
            NoOfPeopleRated = 0,
            Description = @Description
        WHERE BookId = @BookId;

        -- Check if the update was successful
        IF @@ROWCOUNT = 0
        BEGIN
            THROW 50002, 'Update operation failed. No rows were updated.', 1;
        END
    END TRY
    BEGIN CATCH
        -- Capture and handle any errors that occur during the TRY block
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        -- Raise the error with the captured details
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
